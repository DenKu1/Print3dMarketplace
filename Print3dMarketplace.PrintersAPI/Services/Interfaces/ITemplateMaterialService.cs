using Print3dMarketplace.PrintersAPI.Contracts.DTOs;

namespace Print3dMarketplace.PrintersAPI.Services.Interfaces;

public interface ITemplatePrinterService
{
	Task<IEnumerable<TemplatePrinterDto>> GetAllTemplatePrinters();
}
