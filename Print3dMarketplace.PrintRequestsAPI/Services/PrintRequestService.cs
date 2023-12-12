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

	public async Task<IEnumerable<PrintRequestDto>> GetAllPrintRequests()
	{
		var printRequests = await _context.Set<PrintRequest>().AsQueryable().ToListAsync();

		return _mapper.Map<IEnumerable<PrintRequestDto>>(printRequests);
	}

	public async Task<bool> CreatePrintRequest(PrintRequestDto newPrintRequestDto, Guid userId)
	{
		try
		{
			var newPrintRequest = _mapper.Map<PrintRequest>(newPrintRequestDto);
			newPrintRequest.ApplicationUserId = userId;

			await _context.PrintRequests.AddAsync(newPrintRequest);

			return await _context.SaveChangesAsync() > 0;
		}
		catch (Exception)
		{
			return false;
		}
	}

}
