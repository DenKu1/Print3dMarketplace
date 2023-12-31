﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.AuthAPI.Services.Interfaces;
using Print3dMarketplace.Common.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Print3dMarketplace.AuthAPI.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
	private readonly JwtOptions _jwtOptions;
	private readonly ICreatorService _creatorService;

	public JwtTokenGenerator(
		IOptions<JwtOptions> jwtOptions,
		ICreatorService creatorService)
	{
		_jwtOptions = jwtOptions.Value;
		_creatorService = creatorService;
	}

	public async Task<string> GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
	{
		var tokenHandler = new JwtSecurityTokenHandler();

		var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

		var claimList = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Email, applicationUser.Email),
			new(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),
			new(JwtRegisteredClaimNames.Name, applicationUser.UserName)
		};

		claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		if (applicationUser.IsCreator == true)
		{
			var creatorDto = await _creatorService.GetCreator(applicationUser.Id);

			claimList.Add(new Claim(KnownClainTypes.CompanyName.ToString(), creatorDto.CompanyName));
		}

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Audience = _jwtOptions.Audience,
			Issuer = _jwtOptions.Issuer,
			Subject = new ClaimsIdentity(claimList),
			Expires = DateTime.UtcNow.AddDays(7),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}
