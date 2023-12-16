using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.SchemeStorageAPI.Models;

namespace Print3dMarketplace.SchemeStorageAPI.EF;

public class SchemeStorageDbContext : DbContext
{
	public SchemeStorageDbContext(DbContextOptions<SchemeStorageDbContext> options) : base(options)
	{
	}

	public DbSet<Scheme> Schemes { get; set; }
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//modelBuilder.Entity<Scheme>().Property(p => p.Data)
		//			.HasColumnType("MediumBlob");

		base.OnModelCreating(modelBuilder);
	}
}
