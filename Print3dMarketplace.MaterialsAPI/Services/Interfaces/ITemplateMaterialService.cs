using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;

namespace Print3dMarketplace.MaterialsAPI.Services.Interfaces;

public interface ITemplateMaterialService
{
	Task<IEnumerable<TemplateMaterialDto>> GetAllTemplateMaterials();
}
