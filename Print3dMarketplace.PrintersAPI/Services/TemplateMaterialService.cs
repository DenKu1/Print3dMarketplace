using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintersAPI.EF;
using Print3dMarketplace.PrintersAPI.Entities;
using Print3dMarketplace.PrintersAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintersAPI.Services;

public class TemplatePrinterService : ITemplatePrinterService
{
	private readonly IMapper _mapper;

	private readonly PrintersDbContext _context;

	public TemplatePrinterService(
		IMapper mapper,
		PrintersDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<IEnumerable<TemplatePrinterDto>> GetAllTemplatePrinters()
	{
		var templateMaterials = await _context.Set<TemplatePrinter>().AsQueryable().ToListAsync();

		return _mapper.Map<IEnumerable<TemplatePrinterDto>>(templateMaterials);
	}
}
