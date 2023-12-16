using Print3dMarketplace.PrintRequestsAPI.ProxyServices;
using Print3dMarketplace.PrintRequestsAPI.ProxyServices.Interfaces;
using Print3dMarketplace.PrintRequestsAPI.Services;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Startup;

public static class DependencyExtensions
{
	public static void RegisterDependencies(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<IPrintRequestStatusService, PrintRequestStatusService>();
		builder.Services.AddScoped<IPrintRequestService, PrintRequestService>();

		builder.Services.AddScoped<IMaterialProxyService, MaterialProxyService>();
		builder.Services.AddScoped<IPrinterProxyService, PrinterProxyService>();
	}
}
