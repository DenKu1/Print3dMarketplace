using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Print3dMarketplace.Common.ProxyUtilities.Enums;
using Print3dMarketplace.Common.Utility;

namespace Print3dMarketplace.Common.ProxyUtilities;
public static class ProxyServiceStartupExtensions
{

	public static void AddHttpClients(this WebApplicationBuilder builder, IEnumerable<KnownHttpClients> httpClientsToAdd)
	{
		if (httpClientsToAdd is null)
			throw new ArgumentNullException(nameof(httpClientsToAdd));

		builder.Services.AddHttpContextAccessor();
		builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

		foreach (var client in httpClientsToAdd)
		{
			var baseAddress = new Uri(builder.Configuration[$"ServiceUrls:{client}"]);

			builder.Services.AddHttpClient(
				name: client.ToString(),
				configureClient: u => u.BaseAddress = baseAddress)
				.AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
		}
	}
}
