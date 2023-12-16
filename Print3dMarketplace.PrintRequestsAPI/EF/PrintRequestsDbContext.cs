using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.PrintRequestsAPI.Entities;

namespace Print3dMarketplace.PrintRequestsAPI.EF;

public class PrintRequestsDbContext : DbContext
{
	public PrintRequestsDbContext(DbContextOptions<PrintRequestsDbContext> options) : base(options)
	{
	}

	public DbSet<PrintRequest> PrintRequests { get; set; }
	public DbSet<PrintRequestStatus> PrintRequestStatuses { get; set; }
	public DbSet<SubmittedCreator> SubmittedCreators { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PrintRequestStatus>().HasData([
			new() { Id = Guid.NewGuid(), Name = nameof(KnownPrintRequestStatuses.Undefined) },
			new() { Id = Guid.NewGuid(), Name = nameof(KnownPrintRequestStatuses.New) },
			new() { Id = Guid.NewGuid(), Name = nameof(KnownPrintRequestStatuses.Canceled) },
			new() { Id = Guid.NewGuid(), Name = nameof(KnownPrintRequestStatuses.Pending) },
			new() { Id = Guid.NewGuid(), Name = nameof(KnownPrintRequestStatuses.CreatorSubmission) },
			new() { Id = Guid.NewGuid(), Name = nameof(KnownPrintRequestStatuses.UserSubmission) },
			new() { Id = Guid.NewGuid(), Name = nameof(KnownPrintRequestStatuses.Completed) }
		]);

		base.OnModelCreating(modelBuilder);
	}
}
