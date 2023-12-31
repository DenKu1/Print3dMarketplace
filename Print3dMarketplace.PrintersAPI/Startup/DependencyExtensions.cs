﻿using Print3dMarketplace.PrintersAPI.Services;
using Print3dMarketplace.PrintersAPI.Services.Interfaces;

namespace Print3dMarketplace.PrintersAPI.Startup;

public static class DependencyExtensions
{
	public static void RegisterDependencies(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<INozzleService, NozzleService>();
		builder.Services.AddScoped<IPrinterService, PrinterService>();
		builder.Services.AddScoped<ITemplatePrinterService, TemplatePrinterService>();
	}
}
