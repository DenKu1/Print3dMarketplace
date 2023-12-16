using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Print3dMarketplace.AuthAPI.Contracts.DTOs;
using Print3dMarketplace.AuthAPI.Contracts.Enums;
using Print3dMarketplace.AuthAPI.EF;
using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.AuthAPI.Services.Interfaces;

namespace Print3dMarketplace.AuthAPI.Services;

public class AuthService : IAuthService
{
	private readonly IMapper _mapper;
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly ICreatorService _creatorService;

	private readonly AuthDbContext _db;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<ApplicationRole> _roleManager;

	public AuthService(
		IMapper mapper,
		IJwtTokenGenerator jwtTokenGenerator,
		ICreatorService creatorService,
		AuthDbContext db,
		UserManager<ApplicationUser> userManager,
		RoleManager<ApplicationRole> roleManager)
	{
		_mapper = mapper;
		_db = db;
		_jwtTokenGenerator = jwtTokenGenerator;
		_creatorService = creatorService;
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task<IdentityResult> RegisterCustomer(CustomerRegistrationRequestDto registrationRequestDto)
	{
		var customerToAdd = _mapper.Map<ApplicationUser>(registrationRequestDto);
		var result = await _userManager.CreateAsync(customerToAdd, registrationRequestDto.Password);

		if (result.Succeeded)
			await _userManager.AddToRoleAsync(customerToAdd, KnownUserRoles.Customer.ToString());

		return result;
	}

	public async Task<IdentityResult> RegisterCreator(CreatorRegistrationRequestDto registrationRequestDto)
	{
		var creatorToAdd = _mapper.Map<ApplicationUser>(registrationRequestDto);
		var result = await _userManager.CreateAsync(creatorToAdd, registrationRequestDto.Password);

		if (result.Succeeded)
		{
			var createdUser = await _userManager.FindByEmailAsync(registrationRequestDto.Email);

			await _creatorService.AddCreatorInfo(registrationRequestDto, createdUser.Id);

			await _userManager.AddToRoleAsync(creatorToAdd, KnownUserRoles.Creator.ToString());
		}

		return result;
	}

	public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
	{
		var user = _db.ApplicationUsers.FirstOrDefault(
			x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());

		var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

		if (user == null || isValid == false)
			return null;

		var roles = await _userManager.GetRolesAsync(user);

		var token = await _jwtTokenGenerator.GenerateToken(user, roles);

		var userDto = _mapper.Map<UserDto>(user);

		var loginResponseDto = new LoginResponseDto()
		{
			User = userDto,
			Token = token
		};

		return loginResponseDto;
	}
}
