using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintersAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintersAPI.Controllers;

[Route("api/printers/template-printers")]
[ApiController]
public class TemplatePrinterController : ControllerBase
{
	private readonly ITemplatePrinterService _templatePrinterService;

	public TemplatePrinterController(
		ITemplatePrinterService templatePrinterService)
	{
		_templatePrinterService = templatePrinterService;
	}

	[HttpGet]
	public async Task<IActionResult> GetTemplatePrinters()
	{
		var result = await _templatePrinterService.GetAllTemplatePrinters();

		return Ok(ResponseDto<IEnumerable<TemplatePrinterDto>>.SuccessResponse(result));
	}
}

