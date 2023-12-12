namespace Print3dMarketplace.PrintRequestsAPI.Entities;

public enum KnownPrintRequestStatuses
{
	Undefined,
	New,
	Canceled,
	Pending,
	CreatorSubmission,
	UserSubmission,
	Completed
}
