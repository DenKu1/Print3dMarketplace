using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintersAPI.EF;
using Print3dMarketplace.PrintersAPI.Entities;
using Print3dMarketplace.PrintersAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintersAPI.Services;

public class PrinterService : IPrinterService
{
	private readonly IMapper _mapper;

	private readonly PrintersDbContext _context;

	public PrinterService(
		IMapper mapper,
		PrintersDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<IEnumerable<PrinterDto>> GetAllCreatorPrinters(Guid userId)
	{
		var printers = await _context.Set<Printer>()
			.Where(x => x.ApplicationUserId == userId)
			.AsQueryable().ToListAsync();

		return _mapper.Map<IEnumerable<PrinterDto>>(printers);
	}

	public async Task<bool> UpdateCreatorPrinters(IEnumerable<PrinterDto> newPrinterDtos, Guid userId)
	{
		try
		{
			var existingPrinters = await _context.Printers.Where(m => m.ApplicationUserId == userId).ToListAsync();
			_context.Printers.RemoveRange(existingPrinters);

			var newPrinters = _mapper.Map<List<Printer>>(newPrinterDtos);
			newPrinters.ForEach(m => m.ApplicationUserId = userId);

			await _context.Printers.AddRangeAsync(newPrinters);

			await _context.SaveChangesAsync();

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
