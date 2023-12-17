using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;
using Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;

namespace Print3dMarketplace.SchemeStorageAPI.Services;

public class SchemeStorageService : ISchemeStorageService
{
	private readonly IBlobService _stlBlobService;
	public SchemeStorageService(IBlobService stlBlobService) 
	{
		_stlBlobService = stlBlobService
			?? throw new ArgumentNullException(nameof(stlBlobService));
	}

	public async Task<string> UploadScheme(StlSchemeRequestDTO stlSchemeRequest)
	{
		var blobRecordId = $"{stlSchemeRequest.UserId}/{stlSchemeRequest.FileName}_{Guid.NewGuid()}";
		
		await _stlBlobService.UploadAsync(stlSchemeRequest.Data, blobRecordId);

		return blobRecordId;
	}

	public async Task<byte[]> DownloadScheme(string fileName)
	{
		return await _stlBlobService.DownloadAsync(fileName);
	}
}
