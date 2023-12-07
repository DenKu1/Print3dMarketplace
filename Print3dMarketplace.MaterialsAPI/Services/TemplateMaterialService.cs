using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;
using Print3dMarketplace.MaterialsAPI.EF;
using Print3dMarketplace.MaterialsAPI.Entities;
using Print3dMarketplace.MaterialsAPI.Services.Interfaces;

namespace Print3dMarketplace.MaterialsAPI.Services;

public class TemplateMaterialService : ITemplateMaterialService
{
	private readonly IMapper _mapper;

	private readonly MaterialsDbContext _context;

	public TemplateMaterialService(
		IMapper mapper,
		MaterialsDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<IEnumerable<TemplateMaterialDto>> GetAllTemplateMaterials()
	{
		var templateMaterials = await _context.Set<TemplateMaterial>().AsQueryable().ToListAsync();

		return _mapper.Map<IEnumerable<TemplateMaterialDto>>(templateMaterials);
	}
}
