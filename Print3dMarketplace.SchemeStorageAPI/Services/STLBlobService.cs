using Azure.Storage.Blobs;
using Print3dMarketplace.SchemeStorageAPI.Exceptions;
using Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;

namespace Print3dMarketplace.SchemeStorageAPI.Services;

public class STLBlobService : IBlobService
{
	private const string BlobContainerName = "stlcontainer";
	private readonly BlobServiceClient _blobServiceClient;

	public STLBlobService(BlobServiceClient blobServiceClient) 
	{
		_blobServiceClient = blobServiceClient 
			?? throw new ArgumentNullException(nameof(blobServiceClient));
	}

	public async Task UploadAsync(byte[] data, string fileName)
	{
		var blobClient = GetClientContainer(fileName);
		await using var mermoryStream = new MemoryStream(data);
		var contentInfo = await blobClient.UploadAsync(mermoryStream, true);
		var response = contentInfo.GetRawResponse();

		if (response.Status != 201 || response.IsError)
			throw new UploadExceptionBlob($"{response.ReasonPhrase}_{response.ClientRequestId}");
	}

	public async Task<byte []> DownloadAsync(string fileName)
	{
		var blobClient = GetClientContainer(fileName);
		var response = await blobClient.DownloadAsync();

		using var memoryStream = new MemoryStream();
		await response.Value.Content.CopyToAsync(memoryStream);

		return memoryStream.ToArray();
	}

	public async Task<Dictionary<string, byte[]>> GetAllStlFiles(IEnumerable<string> fileNames)
	{
		var blobClient = _blobServiceClient.GetBlobContainerClient(BlobContainerName);
		var dict = new Dictionary<string, byte[]>();

		foreach (var el in fileNames)
		{
			try
			{
				var blob = await DownloadAsync(el);
				dict.Add(el, blob);
			}
			catch (Exception ex)
			{
				return new Dictionary<string, byte[]>();
			}
		}

		return dict;
	}

	public async Task<bool> Delete(string fileName)
	{
		var blobClient = GetClientContainer(fileName);
		var response = await blobClient.DeleteAsync();

		if (response.Status != 201 || response.IsError)
			throw new UploadExceptionBlob($"{response.ReasonPhrase}_{response.ClientRequestId}");

		return true;
	}

	private BlobClient GetClientContainer(string fileName)
	{
		return _blobServiceClient
					.GetBlobContainerClient(BlobContainerName)
					.GetBlobClient(fileName);
	}
}
