using Print3dMarketplace.PrintersAPI.Entities;

namespace Print3dMarketplace.PrintersAPI.Services.Interfaces;

public interface IPrinterService
{
	Task<IEnumerable<PrinterDto>> GetAllCreatorPrinters(Guid userId);

	Task<bool> UpdateCreatorPrinters(IEnumerable<PrinterDto> printers, Guid userId);
}
