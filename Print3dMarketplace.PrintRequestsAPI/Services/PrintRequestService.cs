using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.EF;
using Print3dMarketplace.PrintRequestsAPI.Entities;
using Print3dMarketplace.PrintRequestsAPI.ProxyServices.Interfaces;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Services;

public class PrintRequestService : IPrintRequestService
{
	private readonly IMapper _mapper;

	private readonly PrintRequestsDbContext _context;

	private readonly IMaterialProxyService _materialProxyService;
	private readonly IPrinterProxyService _printerProxyService;


	public PrintRequestService(
		IMapper mapper,
		PrintRequestsDbContext context,
		IMaterialProxyService materialProxyService,
		IPrinterProxyService printerProxyService)
	{
		_mapper = mapper;
		_context = context;
		_materialProxyService = materialProxyService;
		_printerProxyService = printerProxyService;
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
			applicablePrintRequests = await _context.Set<PrintRequest>()
				.AsQueryable()
				.Include(pr => pr.PrintRequestStatus)
				.Include(pr => pr.SubmittedCreators)
				.Where(pr => pr.PrintRequestStatus.Name == KnownPrintRequestStatuses.New.ToString()
					|| pr.PrintRequestStatus.Name == KnownPrintRequestStatuses.CreatorSubmission.ToString())
				.Where(pr => materialIds.Contains(pr.TemplateMaterialId))
				.Where(pr => printerPrintAreaLengths.Any(length => length > pr.PrintAreaLength)
					&& printerPrintAreaWidths.Any(width => width > pr.PrintAreaWidth)
					&& printerPrintAreaHeights.Any(height => height > pr.PrintAreaHeight))
				.ToListAsync();
		}
		catch (Exception ex) { }

		return _mapper.Map<IEnumerable<PrintRequestDto>>(applicablePrintRequests);
	}

	public async Task<bool> CreatePrintRequest(CreatePrintRequestDto newPrintRequestDto, Guid userId)
	{
		try
		{
			var newPrintRequest = _mapper.Map<PrintRequest>(newPrintRequestDto);

			newPrintRequest.ApplicationUserId = userId;
			newPrintRequest.IsActive = true;

			await SetPrintRequestStatus(newPrintRequest, KnownPrintRequestStatuses.New);
			
			await _context.PrintRequests.AddAsync(newPrintRequest);

			return await _context.SaveChangesAsync() > 0;
		}
		catch (Exception)
		{
			return false;
		}
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
				.FirstOrDefaultAsync(x => x.Id == printRequestId);

			if (printRequestToUpdate == null)
				return false;

			// Only New and CreatorSubmission PRs can be submitted by Creator
			if (printRequestToUpdate.PrintRequestStatus.Name != KnownPrintRequestStatuses.New.ToString()
				|| printRequestToUpdate.PrintRequestStatus.Name != KnownPrintRequestStatuses.CreatorSubmission.ToString())
				return false;

			// Check if creator already submitted this PR
			if (printRequestToUpdate.SubmittedCreators.Any(c => c.CreatorId == creatorId))
				return false;

			var submittedCreatorToAdd = new SubmittedCreator
			{
				CreatorId = creatorId,
				CreatorName = companyName
			};

			printRequestToUpdate.SubmittedCreators.Append(submittedCreatorToAdd);

			// If it is the first creator that submitted PR than we move to CreatorSubmission
			if (printRequestToUpdate.PrintRequestStatus.Name == KnownPrintRequestStatuses.New.ToString())
			{
				await SetPrintRequestStatus(printRequestToUpdate, KnownPrintRequestStatuses.CreatorSubmission);

				return await _context.SaveChangesAsync() > 0;
			}

			return true;
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
