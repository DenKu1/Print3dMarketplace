using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.AuthAPI.Entities;

namespace Print3dMarketplace.AuthAPI.EF;

public class AuthDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
	public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
	{
	}

	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<Creator> Creators { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Creator>()
			.HasOne(x => x.ApplicationUser)
			.WithOne(e => e.Creator)
			.HasForeignKey<Creator>(x => x.ApplicationUserId)
			.IsRequired();

		base.OnModelCreating(modelBuilder);
	}
}
