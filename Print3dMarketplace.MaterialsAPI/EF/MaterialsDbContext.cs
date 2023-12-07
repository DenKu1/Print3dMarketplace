using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.MaterialsAPI.Entities;

namespace Print3dMarketplace.MaterialsAPI.EF;

public class MaterialsDbContext : DbContext
{
	public MaterialsDbContext(DbContextOptions<MaterialsDbContext> options) : base(options)
	{
	}

	public DbSet<Color> Colors { get; set; }
	public DbSet<Material> Materials { get; set; }
	public DbSet<TemplateMaterial> TemplateMaterials { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Color>().HasData([
			new() { Id = Guid.NewGuid(), Name = "Red" },
			new() { Id = Guid.NewGuid(), Name = "Green" },
			new() { Id = Guid.NewGuid(), Name = "Blue" },
			new() { Id = Guid.NewGuid(), Name = "White" },
		]);

		modelBuilder.Entity<TemplateMaterial>().HasData([
			new() { Id = Guid.NewGuid(), Name = "ABS" },
			new() { Id = Guid.NewGuid(), Name = "PLA" },
			new() { Id = Guid.NewGuid(), Name = "PETG" }
		]);

		base.OnModelCreating(modelBuilder);
	}
}
