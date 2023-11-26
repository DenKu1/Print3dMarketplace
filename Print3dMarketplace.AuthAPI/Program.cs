using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.AuthAPI.EF;
using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.AuthAPI.Services;
using Print3dMarketplace.AuthAPI.Services.Interfaces;
using Print3dMarketplace.Common.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
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

builder.Services.AddControllers();

RegisterMapper();
RegisterDependencies();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
