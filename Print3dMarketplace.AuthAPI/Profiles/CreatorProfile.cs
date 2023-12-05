using AutoMapper;
using Print3dMarketplace.AuthAPI.Contracts.DTOs;
using Print3dMarketplace.AuthAPI.Entities;

namespace Print3dMarketplace.AuthAPI.Profiles;

public class CreatorProfile : Profile
{
	public CreatorProfile()
	{
		CreateMap<Creator, CreatorDto>();
		CreateMap<CreatorRegistrationRequestDto, Creator>();
	}
}
