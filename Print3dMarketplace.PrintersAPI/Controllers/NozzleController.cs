using Microsoft.AspNetCore.Mvc;
using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintersAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintersAPI.Controllers;

[Route("api/printers/nozzles")]
[ApiController]
public class NozzleController : ControllerBase
{
	private readonly INozzleService _nozzleService;

	public NozzleController(
		INozzleService nozzleService)
	{
		_nozzleService = nozzleService;
	}

	[HttpGet]
	public async Task<IActionResult> GetNozzles()
	{
		var result = await _nozzleService.GetAllNozzles();

		return Ok(ResponseDto<IEnumerable<NozzleDto>>.SuccessResponse(result));
	}
}
