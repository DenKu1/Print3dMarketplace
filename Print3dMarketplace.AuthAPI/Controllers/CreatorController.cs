using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.AuthAPI.Services.Interfaces;
using Print3dMarketplace.Common.DTOs;

namespace Print3dMarketplace.AuthAPI.Controllers;

[Route("api/creator")]
[ApiController]
public class CreatorController : ControllerBase
{
	private readonly IAuthService _authService;

	public CreatorController(
		IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("{userId}")]
	public async Task<IActionResult> RegisterCustomer([FromRoute] int userId)
	{
		var result = await _authService.RegisterCustomer(model);

		if (!result.Succeeded)
			return BadRequest(ResponseDto.ErrorResponse(result.ToString()));

		return Ok(ResponseDto.SuccessResponse());
	}
}
