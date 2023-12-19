using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Print3dMarketplace.Common.ProxyUtilities.Enums;
using Print3dMarketplace.Common.ProxyUtilities;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.EF;
using Print3dMarketplace.PrintRequestsAPI.Entities;
using Print3dMarketplace.PrintRequestsAPI.ProxyServices.Interfaces;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;
using Print3dMarketplace.SchemeStorageAPI.Contracts.Integration;
using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;

namespace Print3dMarketplace.PrintRequestsAPI.Services;

public class PrintRequestService : IPrintRequestService
{
	private readonly IMapper _mapper;

	private readonly PrintRequestsDbContext _context;
	private readonly IHttpClientFactory _httpClientFactory;

	private readonly IMaterialProxyService _materialProxyService;
	private readonly IPrinterProxyService _printerProxyService;


	public PrintRequestService(
		IMapper mapper,
		PrintRequestsDbContext context,
		IMaterialProxyService materialProxyService,
		IPrinterProxyService printerProxyService,
		IHttpClientFactory clientFactory)
	{
		_mapper = mapper;
		_context = context;
		_materialProxyService = materialProxyService;
		_printerProxyService = printerProxyService;
		_httpClientFactory = clientFactory;
	}

	public async Task<IEnumerable<PrintRequestDto>> GetCustomerPrintRequests(Guid customerId)
	{
		var printRequests = await _context.Set<PrintRequest>()
			.AsQueryable()
			.Include(pr => pr.PrintRequestStatus)
			.Include(pr => pr.SubmittedCreators)
			.Where(pr => pr.ApplicationUserId == customerId)
			.ToListAsync();

		return _mapper.Map<IEnumerable<PrintRequestDto>>(printRequests);
	}

	public async Task<IEnumerable<PrintRequestDto>> GetCreatorPrintRequests(Guid creatorId)
	{
		var materials = await _materialProxyService.GetAllCreatorMaterials(creatorId);
		var printers = await _printerProxyService.GetAllCreatorPrinters(creatorId);

		var materialIds = materials.Select(m => m.TemplateMaterialId).ToList();
		var printerPrintAreaLengths = printers.Select(p => p.PrintAreaLength).ToList();
		var printerPrintAreaWidths = printers.Select(p => p.PrintAreaWidth).ToList();
		var printerPrintAreaHeights = printers.Select(p => p.PrintAreaHeight).ToList();

		List<PrintRequest> applicablePrintRequests = [];

		try
		{
			var query = _context.Set<PrintRequest>()
				.AsQueryable()
				.Include(pr => pr.PrintRequestStatus)
				.Include(pr => pr.SubmittedCreators)

				// Show creator only New and CreatorSubmission statuses (already approved by this or other creator)
				// or in case this Creator was choosen by Customer to do this PR
				.Where(pr => pr.PrintRequestStatus.Name == KnownPrintRequestStatuses.New.ToString()
					|| pr.PrintRequestStatus.Name == KnownPrintRequestStatuses.CreatorSubmission.ToString()
					|| (pr.PrintRequestStatus.Name == KnownPrintRequestStatuses.CustomerSubmission.ToString() && pr.CustomerSubmittedCreatorId == creatorId))

				// Filter out PRs that cannot be done due to absence of specific material type requested by Customer
				.Where(pr => materialIds.Contains(pr.TemplateMaterialId))

				// Filter out PRs that cannot be done due to absence of Printer that can print model of such size
				.Where(pr => printerPrintAreaLengths.Any(length => length > pr.PrintAreaLength)
					&& printerPrintAreaWidths.Any(width => width > pr.PrintAreaWidth)
					&& printerPrintAreaHeights.Any(height => height > pr.PrintAreaHeight));

			applicablePrintRequests = await query.ToListAsync();
		}
		catch (Exception ex) { }

		return _mapper.Map<IEnumerable<PrintRequestDto>>(applicablePrintRequests);
	}

	public async Task<FileResonse> GetStlScheme(Guid userId, Guid ModelId)
	{
	   var model = await _context.PrintRequests.FirstOrDefaultAsync(el => el.Id.Equals(ModelId));

	   ArgumentNullException.ThrowIfNull(model);

		var requstModel = new StlSchemeRequestDTO
		{
			Data = [],
			FileName = model.FileName,
			UserId = userId.ToString(),
			ModelID = model.Id.ToString()
		};

		var data = await _httpClientFactory.ClientPostAsync<byte[], StlSchemeRequestDTO>(KnownHttpClients.SchemeStorageAPI,
								KnownSchemeStorageEndpoints.DownloadSTLFile, requstModel) ?? [];

		return new FileResonse
		{
			Data = Convert.ToBase64String(data),
			FileName = model.FileName
		};
	}

	public async Task<bool> CreatePrintRequest(CreatePrintRequestDto newPrintRequestDto, Guid userId)
	{
		try
		{
			ArgumentNullException.ThrowIfNull(newPrintRequestDto);

			var newPrintRequest = _mapper.Map<PrintRequest>(newPrintRequestDto);

			newPrintRequest.ApplicationUserId = userId;
			newPrintRequest.IsActive = true;
			newPrintRequest.FileName = newPrintRequestDto.FileName;

			await SetPrintRequestStatus(newPrintRequest, KnownPrintRequestStatuses.New);
			await _context.PrintRequests.AddAsync(newPrintRequest);

			var result = await _context.SaveChangesAsync() > 0;
			await SaveStlScheme(newPrintRequestDto, userId, newPrintRequest);

			return result;

		}
		catch (Exception)
		{
			return false;
		}
	}

	private async Task SaveStlScheme(CreatePrintRequestDto newPrintRequestDto, Guid userId, PrintRequest newPrintRequest)
	{
		if (newPrintRequestDto.FileName.IsNullOrEmpty() || newPrintRequestDto.FileContent.IsNullOrEmpty())
			throw new InvalidOperationException("File not save because FileName or FileContent is empty");

		var requstModel = new StlSchemeRequestDTO
		{
			Data = Convert.FromBase64String(newPrintRequestDto.FileContent.Substring(newPrintRequestDto.FileContent.IndexOf(',') + 1)),
			FileName = newPrintRequestDto.FileName,
			UserId = userId.ToString(),
			ModelID = newPrintRequest.Id.ToString()
		};
			
		await _httpClientFactory.ClientPostAsync<string, StlSchemeRequestDTO>(KnownHttpClients.SchemeStorageAPI,
								KnownSchemeStorageEndpoints.UploadSTLFile, requstModel);
	}

	public async Task<bool> CancelPrintRequest(Guid printRequestId)
	{
		try
		{
			var printRequestToUpdate = await _context.PrintRequests.FirstOrDefaultAsync(x => x.Id == printRequestId);

			if (printRequestToUpdate == null) 
				return false;

			printRequestToUpdate.IsActive = false;
			await SetPrintRequestStatus(printRequestToUpdate, KnownPrintRequestStatuses.Canceled);

			return await _context.SaveChangesAsync() > 0;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<bool> CreatorSubmitPrintRequest(Guid printRequestId, Guid creatorId, string companyName)
	{
		try
		{
			var printRequestToUpdate = await _context.PrintRequests
				.Include(x => x.PrintRequestStatus)
				.Include(x => x.SubmittedCreators)
				.FirstOrDefaultAsync(x => x.Id == printRequestId);

			if (printRequestToUpdate == null)
				return false;

			// Only New and CreatorSubmission PRs can be submitted by Creator
			if (printRequestToUpdate.PrintRequestStatus.Name != KnownPrintRequestStatuses.New.ToString()
				&& printRequestToUpdate.PrintRequestStatus.Name != KnownPrintRequestStatuses.CreatorSubmission.ToString())
				return false;

			// Check if creator already submitted this PR
			if (printRequestToUpdate.SubmittedCreators.Any(c => c.CreatorId == creatorId))
				return false;

			var submittedCreatorToAdd = new SubmittedCreator
			{
				CreatorId = creatorId,
				CreatorName = companyName,
				PrintRequestId = printRequestId,
			};

			await _context.SubmittedCreators.AddAsync(submittedCreatorToAdd);

			// If it is the first creator that submitted PR than we move to CreatorSubmission
			if (printRequestToUpdate.PrintRequestStatus.Name == KnownPrintRequestStatuses.New.ToString())
			{
				await SetPrintRequestStatus(printRequestToUpdate, KnownPrintRequestStatuses.CreatorSubmission);
			}

			return await _context.SaveChangesAsync() > 0;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<bool> CustomerSubmitPrintRequest(Guid printRequestId, Guid creatorId)
	{
		try
		{
			var printRequestToUpdate = await _context.PrintRequests
				.Include(x => x.PrintRequestStatus)
				.Include(x => x.SubmittedCreators)
				.FirstOrDefaultAsync(x => x.Id == printRequestId);

			if (printRequestToUpdate == null)
				return false;

			// Only CreatorSubmission PRs can be submitted by Customer
			if (printRequestToUpdate.PrintRequestStatus.Name != KnownPrintRequestStatuses.CreatorSubmission.ToString())
				return false;

			var submittedCreator = printRequestToUpdate.SubmittedCreators.FirstOrDefault(x => x.CreatorId == creatorId);

			if (submittedCreator == null)
				return false;

			printRequestToUpdate.CustomerSubmittedCreatorId = submittedCreator.CreatorId;
			
			await SetPrintRequestStatus(printRequestToUpdate, KnownPrintRequestStatuses.CustomerSubmission);

			return await _context.SaveChangesAsync() > 0;
		}
		catch (Exception)
		{
			return false;
		}
	}

	private async Task SetPrintRequestStatus(PrintRequest printRequest, KnownPrintRequestStatuses newStatus)
	{
		if (printRequest == null)
			return;

		printRequest.PrintRequestStatus = await _context.Set<PrintRequestStatus>().FirstAsync(x => x.Name == newStatus.ToString());
	}
}
