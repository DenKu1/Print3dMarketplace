using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Print3dMarketplace.AuthAPI.EF;
using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.AuthAPI.Services;
using Print3dMarketplace.AuthAPI.Services.Interfaces;
using Print3dMarketplace.Common.Middleware;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<AuthDbContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opt =>
{
	opt.Password.RequireDigit = false;
	opt.Password.RequireLowercase = false;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequireUppercase = false;
	opt.Password.RequiredLength = 4;
});

builder.Services.AddCors();
builder.Services.AddControllers();

RegisterMapper();
RegisterDependencies();

builder.Services.AddEndpointsApiExplorer();

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

AddAppAuthetication(builder);

var app = builder.Build();

RegisterMiddleware();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
	if (!app.Environment.IsDevelopment())
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API");
		c.RoutePrefix = "/auth";
	}
});

app.UseCors(builder => builder.AllowAnyOrigin()
	.AllowAnyHeader()
	.AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();

app.Run();

void RegisterMapper()
{
	var assembly = Assembly.GetExecutingAssembly();
	builder.Services.AddAutoMapper(assembly);
}

void RegisterDependencies()
{
	builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
	builder.Services.AddScoped<IAuthService, AuthService>();
	builder.Services.AddScoped<ICreatorService, CreatorService>();
}

void RegisterMiddleware()
{
	app.UseMiddleware<ExceptionHandlerMiddleware>();
}

void ApplyMigration()
{
	using (var scope = app.Services.CreateScope())
	{
		var _db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

		if (_db.Database.GetPendingMigrations().Count() > 0)
			_db.Database.Migrate();
	}
}

void AddAppAuthetication(WebApplicationBuilder builder)
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
