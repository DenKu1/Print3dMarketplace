using Print3dMarketplace.PrintRequestsAPI.Services;
using Print3dMarketplace.PrintRequestsAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintRequestsAPI.Startup;

public static class DependencyExtensions
{
	public static void RegisterDependencies(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<IPrintRequestStatusService, PrintRequestStatusService>();
	}
}
