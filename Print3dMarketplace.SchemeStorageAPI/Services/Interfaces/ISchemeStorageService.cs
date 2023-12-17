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
	/// <param name="stlScheme"></param>
	/// <returns></returns>
	public Task<string> UploadScheme(StlSchemeRequestDTO stlScheme);
}
