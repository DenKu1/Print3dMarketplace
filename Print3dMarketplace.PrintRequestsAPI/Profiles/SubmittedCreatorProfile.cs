using AutoMapper;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Entities;

namespace Print3dMarketplace.PrintRequestsAPI.Profiles;

public class SubmittedCreatorProfile : Profile
{
	public SubmittedCreatorProfile()
	{
		CreateMap<SubmittedCreator, SubmittedCreatorDto>();
	}
}
