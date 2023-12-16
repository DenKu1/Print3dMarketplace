using Microsoft.AspNetCore.Identity;
using Print3dMarketplace.AuthAPI.Contracts.DTOs;

namespace Print3dMarketplace.AuthAPI.Services.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterCustomer(CustomerRegistrationRequestDto registrationRequestDto);
    Task<IdentityResult> RegisterCreator(CreatorRegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
}
