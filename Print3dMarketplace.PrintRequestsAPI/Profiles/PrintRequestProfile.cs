using AutoMapper;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Entities;

namespace Print3dMarketplace.PrintRequestsAPI.Profiles;

public class PrintRequestProfile : Profile
{
	public PrintRequestProfile()
	{
		CreateMap<PrintRequest, PrintRequestDto>().ReverseMap();
	}
}
