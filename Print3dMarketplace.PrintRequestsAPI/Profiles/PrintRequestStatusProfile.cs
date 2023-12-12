using AutoMapper;
using Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.Entities;

namespace Print3dMarketplace.PrintRequestsAPI.Profiles;

public class PrintRequestStatusProfile : Profile
{
	public PrintRequestStatusProfile()
	{
		CreateMap<PrintRequestStatus, PrintRequestStatusDto>();
	}
}
