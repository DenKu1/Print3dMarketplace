using System.Security.Claims;

namespace Print3dMarketplace.Common.Auth;

public static class ClaimsPrincipalExtensions
{
	public static string? GetCompanyName(this ClaimsPrincipal user)
	{
		if (user == null)
			return null;

		return user.Claims.FirstOrDefault(c => c.Type == KnownClainTypes.CompanyName.ToString())?.Value;
	}

	public static Guid GetUserId(this ClaimsPrincipal user)
	{
		if (user == null)
			return Guid.Empty;

		var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

		Guid.TryParse(claim?.Value, out var userId);

		return userId;
	}
}
