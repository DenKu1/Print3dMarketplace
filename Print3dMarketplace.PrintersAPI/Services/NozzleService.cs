using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintersAPI.EF;
using Print3dMarketplace.PrintersAPI.Entities;
using Print3dMarketplace.PrintersAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintersAPI.Services;

public class NozzleService : INozzleService
{
	private readonly IMapper _mapper;

	private readonly PrintersDbContext _context;

	public NozzleService(
		IMapper mapper,
		PrintersDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<IEnumerable<NozzleDto>> GetAllNozzles()
	{
		var nozzles = await _context.Set<Nozzle>().AsQueryable().ToListAsync();

		return _mapper.Map<IEnumerable<NozzleDto>>(nozzles);
	}
}
