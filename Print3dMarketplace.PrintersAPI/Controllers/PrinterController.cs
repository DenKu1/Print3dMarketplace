using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintersAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintersAPI.Controllers;

[Route("api/printers/printers")]
[ApiController]
public class PrinterController : ControllerBase
{
	private readonly IPrinterService _printerService;

	public PrinterController(
		IPrinterService printerService)
	{
		_printerService = printerService;
	}

	[HttpGet("{userId}")]
	public async Task<IActionResult> GetAllCreatorPrinters(Guid userId)
	{
		var printers = await _printerService.GetAllCreatorPrinters(userId);

		return Ok(ResponseDto<IEnumerable<PrinterDto>>.SuccessResponse(printers));
	}

	[HttpPut("{userId}")]
	public async Task<IActionResult> UpdateCreatorPrinters(IEnumerable<PrinterDto> newPrinterDtos, Guid userId)
	{
		var isSuccess = await _printerService.UpdateCreatorPrinters(newPrinterDtos, userId);

		if (!isSuccess)
			return NotFound(ResponseDto.ErrorResponse($"Creator printers for user {userId} were not updated"));

		return Ok(ResponseDto.SuccessResponse());
	}
}
