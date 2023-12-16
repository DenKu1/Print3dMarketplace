using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;

namespace Print3dMarketplace.PrintRequestsAPI.ProxyServices.Interfaces;

public interface IMaterialProxyService
{
	Task<IEnumerable<MaterialDto>> GetAllCreatorMaterials(Guid userId);
}
