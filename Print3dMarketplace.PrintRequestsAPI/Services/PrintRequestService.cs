using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.EF;
using Print3dMarketplace.PrintRequestsAPI.Entities;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Services;

public class PrintRequestService : IPrintRequestService
{
	private readonly IMapper _mapper;

	private readonly PrintRequestsDbContext _context;

	public PrintRequestService(
		IMapper mapper,
		PrintRequestsDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<IEnumerable<PrintRequestDto>> GetCustomerPrintRequests(Guid customerId)
	{
		var printRequests = await _context.Set<PrintRequest>()
			.AsQueryable()
			.Include(x => x.PrintRequestStatus)
			.Where(x => x.ApplicationUserId == customerId)
			.ToListAsync();

		return _mapper.Map<IEnumerable<PrintRequestDto>>(printRequests);
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

	private async Task SetPrintRequestStatus(PrintRequest printRequest, KnownPrintRequestStatuses newStatus)
	{
		if (printRequest == null)
			return;

		printRequest.PrintRequestStatus = await _context.Set<PrintRequestStatus>().FirstAsync(x => x.Name == newStatus.ToString());
	}
}
