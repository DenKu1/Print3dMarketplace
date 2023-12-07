using System.Security.Claims;

namespace Print3dMarketplace.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
/*	public static Guid? GetUserId(this ClaimsPrincipal user)
	{
		if (user == null)
			return null;

		var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

		if (Guid.TryParse(claim?.Value, out var userId))
			return userId;
		else
			return null;
	}
*/
	public static Guid GetUserId(this ClaimsPrincipal user) //OrEmpty
	{
		if (user == null)
			return Guid.Empty;

		var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

		Guid.TryParse(claim?.Value, out var userId);

		return userId;
	}
}
