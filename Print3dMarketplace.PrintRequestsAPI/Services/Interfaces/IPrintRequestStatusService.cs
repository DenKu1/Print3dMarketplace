using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;

namespace Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

public interface IPrintRequestStatusService
{
	Task<IEnumerable<PrintRequestStatusDto>> GetAllPrintRequestStatuses();
}
