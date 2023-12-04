using Print3dMarketplace.Common.Data;

namespace Print3dMarketplace.AuthAPI.Entities;

public class Creator : BaseEntity
{
	public string ApplicationUserId { get; set; }
	public ApplicationUser ApplicationUser { get; set; }
	public string PhoneNumber { get; set; }
	public string AlternativePhoneNumber { get; set; }
	public string Address { get; set; }
	public string Description { get; set; }
}
