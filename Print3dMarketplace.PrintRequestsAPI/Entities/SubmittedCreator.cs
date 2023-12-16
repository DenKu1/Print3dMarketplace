using Print3dMarketplace.Common.Data;

namespace Print3dMarketplace.PrintRequestsAPI.Entities;

public class SubmittedCreator : BaseEntity
{
	public Guid CreatorId { get; set; }
	public string CreatorName { get; set; }

	public Guid PrintRequestId { get; set; }
	public PrintRequest PrintRequest { get; set; }
}
