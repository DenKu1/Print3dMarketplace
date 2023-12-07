using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.Common.Startup;
using Print3dMarketplace.PrintersAPI.EF;
using Print3dMarketplace.PrintersAPI.Startup;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddDbContext<PrintersDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Material API");
		c.RoutePrefix = "/materials";
	}
});

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.ApplyMigration<PrintersDbContext>();

app.Run();
