using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.EF;
using Print3dMarketplace.PrintRequestsAPI.Entities;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Services;

public class PrintRequestStatusService : IPrintRequestStatusService
{
	private readonly IMapper _mapper;

	private readonly PrintRequestsDbContext _context;

	public PrintRequestStatusService(
		IMapper mapper,
		PrintRequestsDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<IEnumerable<PrintRequestStatusDto>> GetAllPrintRequestStatuses()
	{
		var statuses = await _context.Set<PrintRequestStatus>().AsQueryable().ToListAsync();

		return _mapper.Map<IEnumerable<PrintRequestStatusDto>>(statuses);
	}
}
