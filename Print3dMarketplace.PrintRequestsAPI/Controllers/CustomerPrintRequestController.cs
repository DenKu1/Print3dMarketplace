using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.Auth;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Entities;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Controllers;

[Route("api/print-requests/customer/print-requests")]
[ApiController]
public class CustomerPrintRequestController : ControllerBase
{
	private readonly IPrintRequestService _printRequestService;

	public CustomerPrintRequestController(
		IPrintRequestService printRequestService)
	{
		_printRequestService = printRequestService;
	}

	[HttpGet]
	public async Task<IActionResult> GetPrintRequests()
	{
		var result = await _printRequestService.GetCustomerPrintRequests(User.GetUserId());

		return Ok(ResponseDto<IEnumerable<PrintRequestDto>>.SuccessResponse(result));
	}

	[HttpGet("{modelId}/download")]
	public async Task<IActionResult> GetStlScheme(Guid modelId)
	{
		var result = await _printRequestService.GetStlScheme(User.GetUserId(), modelId);

		return Ok(ResponseDto<FileResonse>.SuccessResponse(result));
	}

	[HttpPost]
	public async Task<IActionResult> CreatePrintRequest([FromBody]CreatePrintRequestDto newPrintRequestDto)
	{
		var isSuccess = await _printRequestService.CreatePrintRequest(newPrintRequestDto, User.GetUserId());

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Print request for user {User.GetUserId()} was not created"));

		return Ok(ResponseDto.SuccessResponse());
	}

	[HttpPost("{printRequestId}/cancel")]
	public async Task<IActionResult> CancelPrintRequest(Guid printRequestId)
	{
		var isSuccess = await _printRequestService.CancelPrintRequest(printRequestId);

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Print request for user {User.GetUserId()} was not canceled"));

		return Ok(ResponseDto.SuccessResponse());
	}

	[HttpPost("{printRequestId}/submit")]
	public async Task<IActionResult> CreatorSubmitPrintRequest(Guid printRequestId, SubmitPrintRequestDto submitPrintRequestDto)
	{
		var isSuccess = await _printRequestService.CustomerSubmitPrintRequest(printRequestId, submitPrintRequestDto.CreatorId);

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Print request for customer {User.GetUserId()} was not submitted"));

		return Ok(ResponseDto.SuccessResponse());
	}
}
