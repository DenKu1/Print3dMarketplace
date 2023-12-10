using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;
using Print3dMarketplace.MaterialsAPI.EF;
using Print3dMarketplace.MaterialsAPI.Entities;
using Print3dMarketplace.MaterialsAPI.Services.Interfaces;

namespace Print3dMarketplace.MaterialsAPI.Services;

public class ColorService : IColorService
{
	private readonly IMapper _mapper;

	private readonly MaterialsDbContext _context;

	public ColorService(
		IMapper mapper,
		MaterialsDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<IEnumerable<ColorDto>> GetAllColors()
	{
		var colors = await _context.Set<Color>().AsQueryable().ToListAsync();

		return _mapper.Map<IEnumerable<ColorDto>>(colors);
	}
}
