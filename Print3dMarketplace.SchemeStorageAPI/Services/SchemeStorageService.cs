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

	public async Task UploadScheme(StlSchemeRequestDTO stlSchemeRequest)
	{
		await _stlBlobService.UploadAsync(stlSchemeRequest.Data, CreateBlobStorageKey(stlSchemeRequest));
	}

	public async Task<byte[]> DownloadScheme(StlSchemeRequestDTO stlSchemeRequest)
	{
		return await _stlBlobService.DownloadAsync(CreateBlobStorageKey(stlSchemeRequest));
	}

	private static string CreateBlobStorageKey(StlSchemeRequestDTO stlSchemeRequest)
	{
		return $"{stlSchemeRequest.UserId}/{stlSchemeRequest.FileName}_{stlSchemeRequest.ModelID}";
	}
}
