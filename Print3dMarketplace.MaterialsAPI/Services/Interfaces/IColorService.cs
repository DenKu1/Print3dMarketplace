using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;

namespace Print3dMarketplace.MaterialsAPI.Services.Interfaces;

public interface IColorService
{
	Task<IEnumerable<ColorDto>> GetAllColors();
}
