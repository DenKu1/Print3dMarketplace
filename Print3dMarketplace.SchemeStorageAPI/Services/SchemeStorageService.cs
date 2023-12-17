using AutoMapper;
using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;
using Print3dMarketplace.SchemeStorageAPI.EF;
using Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;

namespace Print3dMarketplace.SchemeStorageAPI.Services;

public class SchemeStorageService : ISchemeStorageService
{
	private readonly IBlobService _stlBlobService;
	private readonly IMapper _mapper;
	private readonly SchemeStorageDbContext _context;

	public SchemeStorageService(IBlobService stlBlobService, IMapper mapper,
		SchemeStorageDbContext context) 
	{
		_mapper = mapper
			?? throw new ArgumentNullException(nameof(mapper));
		_stlBlobService = stlBlobService
			?? throw new ArgumentNullException(nameof(stlBlobService));
		_context = context
			?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<string> UploadScheme(StlSchemeRequestDTO stlSchemeRequest)
	{
		var blobRecordId = $"{stlSchemeRequest.UserId}/{stlSchemeRequest.FileName}_{Guid.NewGuid()}";
		
		await _stlBlobService.UploadAsync(stlSchemeRequest.Data, blobRecordId);

		return blobRecordId;
	}

	public async Task<byte[]> DownloadScheme(Guid userId, Guid TemplateMaterialId)
	{
		var blob = _context.Schemes
			.Where(b => b.UserId.Equals(userId) && b.TemplateMaterialId.Equals(TemplateMaterialId))
			.FirstOrDefault();

		if (blob is null)
			throw new InvalidOperationException($"Scheme isn't found");

		return await _stlBlobService.DownloadAsync(blob.FileName);
	}
}
