using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.Common.Startup;
using Print3dMarketplace.MaterialsAPI.EF;
using Print3dMarketplace.MaterialsAPI.Startup;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddDbContext<MaterialsDbContext>(option =>
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
app.ApplyMigration<MaterialsDbContext>();

app.Run();
