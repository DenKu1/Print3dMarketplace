using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;

namespace Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;

/// <summary>
/// ISchemeStorageService that contains method for working with schemes 
/// </summary>
public interface ISchemeStorageService
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="stlScheme"></param>
	/// <returns></returns>
	public Task UploadScheme(StlSchemeRequestDTO stlScheme);

	/// <summary>
	/// Donwload file by fileName
	/// </summary>
	/// <param name="stlScheme"></param>
	/// <returns>Byte array that can be convert into file</returns>
	public Task<byte[]> DownloadScheme(StlSchemeRequestDTO stlScheme);
}
