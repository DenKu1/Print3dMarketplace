using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.ProxyServices.Interfaces;
using Print3dMarketplace.Common.ProxyUtilities;
using Print3dMarketplace.PrintersAPI.Contracts.Integration;
using Print3dMarketplace.Common.ProxyUtilities.Enums;

namespace Print3dMarketplace.PrintRequestsAPI.ProxyServices;

public class PrinterProxyService : IPrinterProxyService
{
	private readonly IHttpClientFactory _httpClientFactory;

	public PrinterProxyService(IHttpClientFactory clientFactory)
	{
		_httpClientFactory = clientFactory;
	}

	public async Task<IEnumerable<PrinterDto>> GetAllCreatorPrinters(Guid userId)
	{
		var requestUrl = KnownPrinterEndpoints.GetAllCreatorPrinters + $"/{userId}";

		var printers = await _httpClientFactory.ClientGetAsync<IEnumerable<PrinterDto>>(KnownHttpClients.PrintersAPI, requestUrl);

		return printers ?? Enumerable.Empty<PrinterDto>();
	}
}
