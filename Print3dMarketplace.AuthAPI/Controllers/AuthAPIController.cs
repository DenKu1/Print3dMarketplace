using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.AuthAPI.Contracts.DTOs;
using Print3dMarketplace.AuthAPI.Services.Interfaces;
using Print3dMarketplace.Common.DTOs;

namespace Print3dMarketplace.AuthAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthAPIController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthAPIController(
		IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("customer/register")]
	public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegistrationRequestDto model)
	{
		var result = await _authService.RegisterCustomer(model);

		if (!result.Succeeded)
			return BadRequest(ResponseDto.ErrorResponse(result.ToString()));

		return Ok(ResponseDto.SuccessResponse());
	}

	[HttpPost("creator/register")]
	public async Task<IActionResult> RegisterCreator([FromBody] CreatorRegistrationRequestDto model)
	{
		var result = await _authService.RegisterCreator(model);

		if (!result.Succeeded)
			return BadRequest(ResponseDto.ErrorResponse(result.ToString()));

		return Ok(ResponseDto.SuccessResponse());
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
	{
		var loginResponse = await _authService.Login(model);

		if (loginResponse == null)
			return Unauthorized(ResponseDto.ErrorResponse("Username or password is incorrect"));
		
		return Ok(ResponseDto.SuccessResponse(loginResponse));
	}
}
