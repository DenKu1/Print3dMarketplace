using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Print3dMarketplace.AuthAPI.Contracts.DTOs;
using Print3dMarketplace.AuthAPI.EF;
using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.AuthAPI.Services.Interfaces;
using Print3dMarketplace.Common.Data;
using System.Linq.Expressions;

namespace Print3dMarketplace.AuthAPI.Services;

public class CreatorService //: ServiceBase<>
{
	private readonly IMapper _mapper;
	private readonly IJwtTokenGenerator _jwtTokenGenerator;

	private readonly AuthDbContext _db;

	public CreatorService(
		IMapper mapper,
		AuthDbContext db)// : base
	{
		_mapper = mapper;
		_db = db;
	}

	public async Task<IdentityResult> GetCreator(string id)
	{
		return await context.Set<Creator>().Where(expression).FirstOrDefaultAsync();


		var user = _mapper.Map<ApplicationUser>(registrationRequestDto);
		return await _userManager.CreateAsync(user, registrationRequestDto.Password);
	}
}
