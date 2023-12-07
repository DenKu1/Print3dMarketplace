using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.AuthAPI.EF;
using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.AuthAPI.Startup;
using Print3dMarketplace.Common.Startup;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();

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
builder.Services.AddEndpointsApiExplorer();

builder.RegisterMapper(assembly);
builder.RegisterDependencies();
builder.AddSwaggerGen();
builder.AddAppAuthentication();

var app = builder.Build();

app.RegisterCommonMiddleware();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
	if (!app.Environment.IsDevelopment())
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API");
		c.RoutePrefix = "/auth";
	}
});

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.ApplyMigration<AuthDbContext>();

app.Run();
