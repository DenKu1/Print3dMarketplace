using Microsoft.IdentityModel.Tokens;
using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;
using Print3dMarketplace.SchemeStorageAPI.Helpers.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Print3dMarketplace.SchemeStorageAPI.Helpers;

public class Guard : IGuard
{
	public void ValidateFileName(string fileName)
	{
		Regex regex = new Regex(@"[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}/[^/]+\.stl
									_[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}");
		
		if (fileName.IsNullOrEmpty() || !regex.Match(fileName).Success)
			throw new ValidationException("fileName have another Pattern");
	}

	public void ValidateStlModel(StlSchemeRequestDTO stlScheme)
	{
		if (stlScheme is null
				|| stlScheme.Data is null
				|| stlScheme.Data.Length.Equals("0")
				|| stlScheme.FileName.IsNullOrEmpty())
			throw new ValidationException("Validation of uploaded file failed");
	}
}
