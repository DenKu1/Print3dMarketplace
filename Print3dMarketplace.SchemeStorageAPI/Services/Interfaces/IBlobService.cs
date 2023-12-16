namespace Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IBlobService
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="data"></param>
	/// <param name="fileName"></param>
	/// <returns></returns>
	Task UploadAsync(byte[] data, string fileName);
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="fileName"></param>
	/// <returns></returns>
	Task<byte[]> DownloadAsync(string fileName);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="fileNames"></param>
	/// <returns></returns>
	Task<Dictionary<string, byte[]>> GetAllStlFiles(IEnumerable<string> fileNames);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="fileName"></param>
	/// <returns></returns>
	Task<bool> Delete(string fileName);
}
