using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Print3dMarketplace.Common.Middleware;
using System.Reflection;
using System.Text;

namespace Print3dMarketplace.Common.Startup;
public static class StartupExtensions
{
	public static void RegisterMapper(this WebApplicationBuilder builder, Assembly currentAssembly)
	{
		builder.Services.AddAutoMapper(currentAssembly);
	}

	public static void AddAppAuthentication(this WebApplicationBuilder builder)
	{
		var settingsSection = builder.Configuration.GetSection("ApiSettings:JwtOptions");

		var secret = settingsSection.GetValue<string>("Secret");
		var issuer = settingsSection.GetValue<string>("Issuer");
		var audience = settingsSection.GetValue<string>("Audience");

		var key = Encoding.ASCII.GetBytes(secret);

		builder.Services.AddAuthentication(x =>
		{
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(x =>
		{
			x.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidIssuer = issuer,
				ValidAudience = audience,
				ValidateAudience = true
			};
		});
	}

	public static void AddSwaggerGen(this WebApplicationBuilder builder)
	{
		builder.Services.AddSwaggerGen(option =>
		{
			option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer"
			});
			option.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference= new OpenApiReference
					{
						Type=ReferenceType.SecurityScheme,
						Id=JwtBearerDefaults.AuthenticationScheme
					}
				}, new string[]{}
			}
		});
		});
	}

	public static void RegisterCommonMiddleware(this WebApplication app)
	{
		app.UseMiddleware<ExceptionHandlerMiddleware>();
	}

	public static void UseCors(this WebApplication app)
	{
		app.UseCors(builder => builder.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod());
	}

	public static void ApplyMigration<TContext>(this WebApplication app) where TContext : DbContext
	{
		using var scope = app.Services.CreateScope();

		var _db = scope.ServiceProvider.GetRequiredService<TContext>();

		if (_db.Database.GetPendingMigrations().Count() > 0)
			_db.Database.Migrate();
	}
}
