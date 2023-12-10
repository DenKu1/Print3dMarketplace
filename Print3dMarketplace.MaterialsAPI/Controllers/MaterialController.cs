using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;
using Print3dMarketplace.MaterialsAPI.Entities;
using Print3dMarketplace.MaterialsAPI.Services.Interfaces;

namespace Print3dMarketplace.MaterialsAPI.Controllers;

[Route("api/materials/materials")]
[ApiController]
public class MaterialController : ControllerBase
{
	private readonly IMaterialService _materialService;

	public MaterialController(
		IMaterialService materialService)
	{
		_materialService = materialService;
	}

	[HttpGet("{userId}")]
	public async Task<IActionResult> GetAllCreatorMaterials(Guid userId)
	{
		var materials = await _materialService.GetAllCreatorMaterials(userId);

		return Ok(ResponseDto.SuccessResponse(materials));
	}

	[HttpPut("{userId}")]
	public async Task<IActionResult> UpdateCreatorMaterials(IEnumerable<MaterialDto> newMaterialDtos, Guid userId)
	{
		var isSuccess = await _materialService.UpdateCreatorMaterials(newMaterialDtos, userId);

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Creator materials for user {userId} were not updated"));

		return Ok(ResponseDto.SuccessResponse());
	}
}

