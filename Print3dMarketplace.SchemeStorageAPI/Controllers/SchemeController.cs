using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;
using Print3dMarketplace.SchemeStorageAPI.Helpers.Interfaces;
using Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;

namespace Print3dMarketplace.SchemeStorageAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SchemeController : ControllerBase
{
	private readonly ISchemeStorageService _schemeStorageService;
	private readonly IGuard _guard;

	public SchemeController(ISchemeStorageService schemeStorageService,
		IGuard guard)
	{
		_schemeStorageService = schemeStorageService 
			?? throw new  ArgumentNullException(nameof(schemeStorageService));
		_guard = guard ?? throw	new ArgumentNullException(nameof(schemeStorageService));
	}

	[HttpPost("upload")]
	public async Task<IActionResult> UploadBlob([FromBody] StlSchemeRequestDTO stlScheme)
	{
		_guard.ValidateStlModel(stlScheme);

		var fileName = await _schemeStorageService.UploadScheme(stlScheme);

		if (!fileName.IsNullOrEmpty())
			return Ok(ResponseDto<string>.SuccessResponse(fileName));

		return BadRequest(ResponseDto.ErrorResponse()); 
	}

	[HttpGet("download")]
	public async Task<IActionResult> DownloadBlob(string fileName)
	{
		_guard.ValidateFileName(fileName);

		var byteArray = await _schemeStorageService.DownloadScheme(fileName);

		if (byteArray is null)
			return BadRequest(ResponseDto.ErrorResponse());

		return Ok(ResponseDto<byte[]>.SuccessResponse(byteArray));
	}
}
