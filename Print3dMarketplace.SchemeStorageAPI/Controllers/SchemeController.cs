using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.Auth;
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

		await _schemeStorageService.UploadScheme(User.GetUserId(), stlScheme);

		if (await _schemeStorageService.UploadScheme(User.GetUserId(), stlScheme))
			return Ok(ResponseDto.SuccessResponse());

		return BadRequest(ResponseDto.ErrorResponse()); 
	}
}
