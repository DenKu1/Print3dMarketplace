using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Controllers;

[Route("api/print-requests/statuses")]
[ApiController]
public class PrintRequestStatusController : ControllerBase
{
	private readonly IPrintRequestStatusService _printRequestStatusService;

	public PrintRequestStatusController(
		IPrintRequestStatusService printRequestStatusService)
	{
		_printRequestStatusService = printRequestStatusService;
	}

	[HttpGet]
	public async Task<IActionResult> GetPrintRequestStatuses()
	{
		var result = await _printRequestStatusService.GetAllPrintRequestStatuses();

		return Ok(ResponseDto<IEnumerable<PrintRequestStatusDto>>.SuccessResponse(result));
	}
}
