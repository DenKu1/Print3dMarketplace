using Print3dMarketplace.PrintersAPI.Contracts.DTOs;

namespace Print3dMarketplace.PrintRequestsAPI.ProxyServices.Interfaces;

public interface IPrinterProxyService
{
	Task<IEnumerable<PrinterDto>> GetAllCreatorPrinters(Guid userId);
}
