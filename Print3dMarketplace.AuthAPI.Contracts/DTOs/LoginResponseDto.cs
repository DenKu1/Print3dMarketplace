namespace Print3dMarketplace.AuthAPI.Contracts.DTOs;

public class LoginResponseDto
{
	public UserDto User { get; set; }
	public string Token { get; set; }
}
