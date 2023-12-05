using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.AuthAPI.Services.Interfaces;
using Print3dMarketplace.Common.DTOs;

namespace Print3dMarketplace.AuthAPI.Controllers;

[Route("api/auth/creator")]
[ApiController]
public class CreatorController : ControllerBase
{
	private readonly ICreatorService _creatorService;

	public CreatorController(
		ICreatorService creatorService)
	{
		_creatorService = creatorService;
	}

	[HttpGet("{userId}")]
	public async Task<IActionResult> GetCreator([FromRoute] Guid userId)
	{
		var result = await _creatorService.GetCreator(userId);

		if (result == null)
			return NotFound(ResponseDto.ErrorResponse($"Creator information for {userId} not found"));

		return Ok(ResponseDto.SuccessResponse(result));
	}
}
