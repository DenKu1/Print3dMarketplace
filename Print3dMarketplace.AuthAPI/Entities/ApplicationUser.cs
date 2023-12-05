using Microsoft.AspNetCore.Identity;

namespace Print3dMarketplace.AuthAPI.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
	public string Name { get; set; }
	public bool? IsCreator { get; set; }
	public Creator Creator { get; set; }
}
