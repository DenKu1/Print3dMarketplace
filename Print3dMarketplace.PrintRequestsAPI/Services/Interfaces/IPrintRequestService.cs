﻿using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;

namespace Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

public interface IPrintRequestService
{
	Task<IEnumerable<PrintRequestDto>> GetAllPrintRequests();

	Task<bool> CreatePrintRequest(CreatePrintRequestDto newPrintRequestDto, Guid userId);
	Task<bool> CancelPrintRequest(Guid printRequestId);
}
