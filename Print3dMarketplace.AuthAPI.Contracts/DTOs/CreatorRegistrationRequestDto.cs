﻿namespace Print3dMarketplace.AuthAPI.Contracts.DTOs;

public class CreatorRegistrationRequestDto
{
    public string Name { get; set; }
    public string Email { get; set; }
	public string Password { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}
