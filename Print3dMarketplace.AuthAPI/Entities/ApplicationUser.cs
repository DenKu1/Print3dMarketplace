using Microsoft.AspNetCore.Identity;

namespace Print3dMarketplace.AuthAPI.Entities;

public class ApplicationUser : IdentityUser
{
	public string Name { get; set; }
	public bool? IsCreator { get; set; }
}
