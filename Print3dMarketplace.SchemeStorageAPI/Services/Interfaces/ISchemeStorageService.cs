using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;

namespace Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;

/// <summary>
///
/// </summary>
public interface ISchemeStorageService
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="userId"></param>
	/// <param name="stlScheme"></param>
	/// <returns></returns>
	public Task<bool> UploadScheme(Guid userId, StlSchemeRequestDTO stlScheme);
}
