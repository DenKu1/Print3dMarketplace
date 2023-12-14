using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.Common.Extensions;
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
	public async Task<IActionResult> GetCustomerPrintRequests()
	{
		var result = await _printRequestService.GetCustomerPrintRequests(User.GetUserId());

		return Ok(ResponseDto.SuccessResponse(result));
	}

	[HttpPost]
	public async Task<IActionResult> CreatePrintRequest(CreatePrintRequestDto newPrintRequestDto)
	{
		var isSuccess = await _printRequestService.CreatePrintRequest(newPrintRequestDto, User.GetUserId());

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Print request was for user {User.GetUserId()} was not created"));

		return Ok(ResponseDto.SuccessResponse());
	}

	[HttpPost("{printRequestId}/cancel")]
	public async Task<IActionResult> CancelPrintRequest(Guid printRequestId)
	{
		var isSuccess = await _printRequestService.CancelPrintRequest(printRequestId);

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Print request was for user {User.GetUserId()} was not created"));

		return Ok(ResponseDto.SuccessResponse());
	}
}
