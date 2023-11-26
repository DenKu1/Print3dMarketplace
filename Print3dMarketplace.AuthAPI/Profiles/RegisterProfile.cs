using AutoMapper;
using Print3dMarketplace.AuthAPI.Contracts.DTOs;
using Print3dMarketplace.AuthAPI.Entities;

namespace Print3dMarketplace.AuthAPI.Profiles;

public class RegisterProfile : Profile
{
	public RegisterProfile()
	{
		CreateMap<CustomerRegistrationRequestDto, ApplicationUser>()
			.ForMember(x => x.UserName, x => x.MapFrom(x => x.Email))
			.ForMember(x => x.NormalizedEmail, x => x.MapFrom(x => x.Email.ToUpper()))
			.ForMember(x => x.UserName, x => x.MapFrom(x => x.Email))
			.ForMember(x => x.IsCreator, x => x.MapFrom(x => false));

		CreateMap<CreatorRegistrationRequestDto, ApplicationUser>()
			.ForMember(x => x.UserName, x => x.MapFrom(x => x.Email))
			.ForMember(x => x.NormalizedEmail, x => x.MapFrom(x => x.Email.ToUpper()))
			.ForMember(x => x.UserName, x => x.MapFrom(x => x.Email))
			.ForMember(x => x.IsCreator, x => x.MapFrom(x => true));

		CreateMap<ApplicationUser, UserDto>();
	}
}
