using Print3dMarketplace.AuthAPI.Services.Interfaces;
using Print3dMarketplace.AuthAPI.Services;

namespace Print3dMarketplace.AuthAPI.Startup;

public static class DependencyExtensions
{
	public static void RegisterDependencies(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
		builder.Services.AddScoped<IAuthService, AuthService>();
		builder.Services.AddScoped<ICreatorService, CreatorService>();
	}
}
