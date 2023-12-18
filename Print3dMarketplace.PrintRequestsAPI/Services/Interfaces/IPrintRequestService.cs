using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Entities;

namespace Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

public interface IPrintRequestService
{
	Task<IEnumerable<PrintRequestDto>> GetCustomerPrintRequests(Guid userId);
	Task<FileResonse> GetStlScheme(Guid userId, Guid ModelId);

	Task<bool> CreatePrintRequest(CreatePrintRequestDto newPrintRequestDto, Guid userId);
	Task<bool> CancelPrintRequest(Guid printRequestId);
	Task<IEnumerable<PrintRequestDto>> GetCreatorPrintRequests(Guid creatorId);
	Task<bool> CreatorSubmitPrintRequest(Guid printRequestId, Guid creatorId, string companyName);
	Task<bool> CustomerSubmitPrintRequest(Guid printRequestId, Guid creatorId);
}
