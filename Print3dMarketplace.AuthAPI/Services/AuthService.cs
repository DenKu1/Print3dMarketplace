using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Print3dMarketplace.AuthAPI.Contracts.DTOs;
using Print3dMarketplace.AuthAPI.EF;
using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.AuthAPI.Services.Interfaces;

namespace Print3dMarketplace.AuthAPI.Services;

public class AuthService : IAuthService
{
	private readonly IMapper _mapper;
	private readonly IJwtTokenGenerator _jwtTokenGenerator;

	private readonly AuthDbContext _db;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public AuthService(
		IMapper mapper,
		IJwtTokenGenerator jwtTokenGenerator,
		AuthDbContext db,
		UserManager<ApplicationUser> userManager,
		RoleManager<IdentityRole> roleManager)
	{
		_mapper = mapper;
		_db = db;
		_jwtTokenGenerator = jwtTokenGenerator;
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task<IdentityResult> RegisterCustomer(CustomerRegistrationRequestDto registrationRequestDto)
	{
		var user = _mapper.Map<ApplicationUser>(registrationRequestDto);
		return await _userManager.CreateAsync(user, registrationRequestDto.Password);
	}

	public async Task<IdentityResult> RegisterCreator(CreatorRegistrationRequestDto registrationRequestDto)
	{
		var user = _mapper.Map<ApplicationUser>(registrationRequestDto);
		return await _userManager.CreateAsync(user, registrationRequestDto.Password);
	}

	public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
	{
		var user = _db.ApplicationUsers.FirstOrDefault(
			x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());

		var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

		if (user == null || isValid == false)
			return null;

		var roles = await _userManager.GetRolesAsync(user);
		var token = _jwtTokenGenerator.GenerateToken(user, roles);

		var userDto = _mapper.Map<UserDto>(user);

		var loginResponseDto = new LoginResponseDto()
		{
			User = userDto,
			Token = token
		};

		return loginResponseDto;
	}

	public Task<bool> AssignRole(string email, string roleName)
	{
		throw new NotImplementedException();
	}
}
