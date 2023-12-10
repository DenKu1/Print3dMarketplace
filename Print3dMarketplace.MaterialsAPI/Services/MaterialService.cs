using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;
using Print3dMarketplace.MaterialsAPI.EF;
using Print3dMarketplace.MaterialsAPI.Entities;
using Print3dMarketplace.MaterialsAPI.Services.Interfaces;

namespace Print3dMarketplace.MaterialsAPI.Services;

public class MaterialService : IMaterialService
{
	private readonly IMapper _mapper;

	private readonly MaterialsDbContext _context;

	public MaterialService(
		IMapper mapper,
		MaterialsDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<IEnumerable<MaterialDto>> GetAllCreatorMaterials(Guid userId)
	{
		var materials = await _context.Set<Material>()
			.Where(x => x.ApplicationUserId == userId)
			.AsQueryable().ToListAsync();

		return _mapper.Map<IEnumerable<MaterialDto>>(materials);
	}

	public async Task<bool> UpdateCreatorMaterials(IEnumerable<MaterialDto> newMaterialDtos, Guid userId)
	{
		try
		{
			var existingMaterials = await _context.Materials.Where(m => m.ApplicationUserId == userId).ToListAsync();
			_context.Materials.RemoveRange(existingMaterials);

			var newMaterials = _mapper.Map<List<Material>>(newMaterialDtos);
			newMaterials.ForEach(m => m.ApplicationUserId = userId);

			await _context.Materials.AddRangeAsync(newMaterials);

			await _context.SaveChangesAsync();

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
