using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.PrintersAPI.Entities;

namespace Print3dMarketplace.PrintersAPI.EF;

public class PrintersDbContext : DbContext
{
	public PrintersDbContext(DbContextOptions<PrintersDbContext> options) : base(options)
	{
	}

	public DbSet<Nozzle> Nozzles { get; set; }
	public DbSet<TemplatePrinter> TemplatePrinters { get; set; }
	public DbSet<Printer> Printers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var nozzle02 = new Nozzle() { Id = Guid.NewGuid(), Size = "0.2mm Nozzle" };
		var nozzle03 = new Nozzle() { Id = Guid.NewGuid(), Size = "0.3mm Nozzle" };
		var nozzle04 = new Nozzle() { Id = Guid.NewGuid(), Size = "0.4mm Nozzle" };
		var nozzle05 = new Nozzle() { Id = Guid.NewGuid(), Size = "0.5mm Nozzle" };
		var nozzle06 = new Nozzle() { Id = Guid.NewGuid(), Size = "0.6mm Nozzle" };
		var nozzle08 = new Nozzle() { Id = Guid.NewGuid(), Size = "0.8mm Nozzle" };
		var nozzle10 = new Nozzle() { Id = Guid.NewGuid(), Size = "1.0mm Nozzle" };

		modelBuilder.Entity<Nozzle>().HasData([
			nozzle02,
			nozzle03,
			nozzle04,
			nozzle05,
			nozzle06,
			nozzle08,
			nozzle10,
		]);

		modelBuilder.Entity<TemplatePrinter>().HasData([
			new()
			{
				Id = Guid.NewGuid(),
				ModelName = "Ender i3",
				PrintAreaLength = 150,
				PrintAreaWidth = 150,
				PrintAreaHeight = 150
			},
			new()
			{
				Id = Guid.NewGuid(),
				ModelName = "Prusa 360",
				PrintAreaLength = 200,
				PrintAreaWidth = 200,
				PrintAreaHeight = 200
			},
			new()
			{
				Id = Guid.NewGuid(),
				ModelName = "Anet 300",
				PrintAreaLength = 300,
				PrintAreaWidth = 300,
				PrintAreaHeight = 300
			}
		]);

		base.OnModelCreating(modelBuilder);
	}
}
