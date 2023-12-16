using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.Auth;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Controllers;

[Route("api/print-requests/creator/print-requests")]
[ApiController]
public class CreatorPrintRequestController : ControllerBase
{
	private readonly IPrintRequestService _printRequestService;

	public CreatorPrintRequestController(
		IPrintRequestService printRequestService)
	{
		_printRequestService = printRequestService;
	}

	[HttpGet]
	public async Task<IActionResult> GetPrintRequests()
	{
		var result = await _printRequestService.GetCreatorPrintRequests(User.GetUserId());

		return Ok(ResponseDto<IEnumerable<PrintRequestDto>>.SuccessResponse(result));
	}

	[HttpPost("{printRequestId}/submit")]
	public async Task<IActionResult> CreatorSubmitPrintRequest(Guid printRequestId)
	{
		var isSuccess = await _printRequestService.CreatorSubmitPrintRequest(printRequestId, User.GetUserId(), User.GetCompanyName());

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Print request for creator {User.GetUserId()} was not submitted"));

		return Ok(ResponseDto.SuccessResponse());
	}
}
