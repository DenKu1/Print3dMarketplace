using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.Common.Startup;
using Print3dMarketplace.SchemeStorageAPI.EF;
using System.Reflection;
using Print3dMarketplace.SchemeStorageAPI.Startup;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddDbContext<SchemeStorageDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorageConnectionString")));
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
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Printer API");
		c.RoutePrefix = "/printers";
	}
});

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.ApplyMigration<SchemeStorageDbContext>();

app.Run();