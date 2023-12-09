using Print3dMarketplace.MaterialsAPI.Services;
using Print3dMarketplace.MaterialsAPI.Services.Interfaces;

namespace Print3dMarketplace.MaterialsAPI.Startup;

public static class DependencyExtensions
{
	public static void RegisterDependencies(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<ITemplateMaterialService, TemplateMaterialService>();
		builder.Services.AddScoped<IMaterialService, MaterialService>();
	}
}
