using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.MaterialsAPI.Services.Interfaces;

namespace Print3dMarketplace.MaterialsAPI.Controllers;

[Route("api/materials/template-materials")]
[ApiController]
public class TemplateMaterialController : ControllerBase
{
	private readonly ITemplateMaterialService _templateMaterial;

	public TemplateMaterialController(
		ITemplateMaterialService templateMaterial)
	{
		_templateMaterial = templateMaterial;
	}

	[HttpGet]
	public async Task<IActionResult> GetTemplateMaterials()
	{
		var result = await _templateMaterial.GetAllTemplateMaterials();

		return Ok(ResponseDto.SuccessResponse(result));
	}
}

