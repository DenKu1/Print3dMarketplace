using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;

namespace Print3dMarketplace.MaterialsAPI.Services.Interfaces;

public interface IMaterialService
{
	Task<bool> UpdateCreatorMaterials(IEnumerable<MaterialDto> materials, Guid userId);
}
