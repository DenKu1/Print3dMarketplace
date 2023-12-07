using AutoMapper;
using Print3dMarketplace.PrintersAPI.Contracts.DTOs;
using Print3dMarketplace.PrintersAPI.Entities;

namespace Print3dMarketplace.PrintersAPI.Profiles;

public class TemplatePrinterProfile : Profile
{
	public TemplatePrinterProfile()
	{
		CreateMap<TemplatePrinter, TemplatePrinterDto>();
	}
}
