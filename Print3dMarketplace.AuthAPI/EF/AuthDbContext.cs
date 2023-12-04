using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.AuthAPI.Entities;

namespace Print3dMarketplace.AuthAPI.EF;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
	public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
	{
	}

	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<Creator> Creators { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
