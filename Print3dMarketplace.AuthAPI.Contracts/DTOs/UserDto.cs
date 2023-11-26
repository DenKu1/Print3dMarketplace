namespace Print3dMarketplace.AuthAPI.Contracts.DTOs;

public class UserDto
{
	public string Id { get; set; }
	public string Email { get; set; }
	public string Name { get; set; }
	public bool? IsCreator { get; set; }
}
