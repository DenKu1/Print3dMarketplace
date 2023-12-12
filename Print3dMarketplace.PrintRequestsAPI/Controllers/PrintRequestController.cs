using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Controllers;

[Route("api/print-requests/print-requests")]
[ApiController]
public class PrintRequestController : ControllerBase
{
	private readonly IPrintRequestService _printRequestService;

	public PrintRequestController(
		IPrintRequestService printRequestService)
	{
		_printRequestService = printRequestService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllPrintRequests()
	{
		var result = await _printRequestService.GetAllPrintRequests();

		return Ok(ResponseDto.SuccessResponse(result));
	}

	[HttpPost("{userId}")]
	public async Task<IActionResult> CreatePrintRequest(PrintRequestDto newPrintRequestDto, Guid userId)
	{
		var isSuccess = await _printRequestService.CreatePrintRequest(newPrintRequestDto, userId);

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Print request was for user {userId} was not created"));

		return Ok(ResponseDto.SuccessResponse());
	}
}
