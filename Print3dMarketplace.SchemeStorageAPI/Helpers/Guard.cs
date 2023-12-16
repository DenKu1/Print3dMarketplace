using Microsoft.IdentityModel.Tokens;
using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;
using Print3dMarketplace.SchemeStorageAPI.Helpers.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Print3dMarketplace.SchemeStorageAPI.Helpers;

public class Guard : IGuard
{
	public void ValidateStlModel(StlSchemeRequestDTO stlScheme)
	{
		if (stlScheme is null
				|| stlScheme.Data is null
				|| stlScheme.Data.Length.Equals("0")
				|| stlScheme.FileName.IsNullOrEmpty())
			throw new ValidationException("Validation of uploaded file failed");
	}
}
