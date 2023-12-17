using Print3dMarketplace.SchemeStorageAPI.Helpers.Interfaces;
using Print3dMarketplace.SchemeStorageAPI.Helpers;
using Print3dMarketplace.SchemeStorageAPI.Services.Interfaces;
using Print3dMarketplace.SchemeStorageAPI.Services;
using Azure.Storage.Blobs;

namespace Print3dMarketplace.SchemeStorageAPI.Startup;

public static class DependencyExtensions
{
	public static void RegisterDependencies(this WebApplicationBuilder builder)
	{
		builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
		builder.Services.AddScoped<IBlobService, STLBlobService>();
		builder.Services.AddScoped<ISchemeStorageService, SchemeStorageService>();
		builder.Services.AddSingleton<IGuard, Guard>();
	}
}
