using AutoMapper;
using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;
using Print3dMarketplace.SchemeStorageAPI.EF;
using Print3dMarketplace.SchemeStorageAPI.Models;
using Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;

namespace Print3dMarketplace.SchemeStorageAPI.Services;

public class SchemeStorageService : ISchemeStorageService
{
	private readonly STLBlobService _sTLBlobService;
	private readonly IMapper _mapper;
	private readonly SchemeStorageDbContext _context;

	public SchemeStorageService(STLBlobService sTLBlobService, IMapper mapper,
		SchemeStorageDbContext context) 
	{
		_mapper = mapper
			?? throw new ArgumentNullException(nameof(mapper));
		_sTLBlobService = sTLBlobService 
			?? throw new ArgumentNullException(nameof(sTLBlobService));
		_context = context
			?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<bool> UploadScheme(Guid userId, StlSchemeRequestDTO stlSchemeRequest)
	{
		var blobRecordId = $"{userId}/{stlSchemeRequest.FileName}_{Guid.NewGuid()}";
		await _sTLBlobService.UploadAsync(stlSchemeRequest.Data, blobRecordId);

		var scheme = _mapper.Map<Scheme>(stlSchemeRequest);
		scheme.UserId = userId;
		scheme.FileName = blobRecordId;

		await _context.AddAsync(scheme);

		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<byte[]> DownloadScheme(Guid userId, Guid TemplateMaterialId)
	{
		var blob = _context.Schemes
			.Where(b => b.UserId.Equals(userId) && b.TemplateMaterialId.Equals(TemplateMaterialId))
			.FirstOrDefault();

		if (blob is null)
			throw new InvalidOperationException($"Scheme isn't found");

		return await _sTLBlobService.DownloadAsync(blob.FileName);
	}
}
