using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.MaterialsAPI.Services.Interfaces;

namespace Print3dMarketplace.MaterialsAPI.Controllers;

[Route("api/materials/colors")]
[ApiController]
public class ColorController : ControllerBase
{
	private readonly IColorService _colorService;

	public ColorController(
		IColorService colorService)
	{
		_colorService = colorService;
	}

	[HttpGet]
	public async Task<IActionResult> GetColors()
	{
		var result = await _colorService.GetAllColors();

		return Ok(ResponseDto.SuccessResponse(result));
	}
}
