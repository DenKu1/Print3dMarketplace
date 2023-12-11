using AutoMapper;
using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintersAPI.Entities;

namespace Print3dMarketplace.PrintersAPI.Profiles;

public class NozzleProfile : Profile
{
	public NozzleProfile()
	{
		CreateMap<Nozzle, NozzleDto>();
	}
}
