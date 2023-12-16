using Print3dMarketplace.AuthAPI.Entities;

namespace Print3dMarketplace.AuthAPI.Services.Interfaces;

public interface IJwtTokenGenerator
{
	Task<string> GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}
