using AutoMapper;
using Print3dMarketplace.PrintersAPI.Entities;

namespace Print3dMarketplace.PrintersAPI.Profiles;

public class PrinterProfile : Profile
{
	public PrinterProfile()
	{
		CreateMap<PrinterDto, Printer>().ReverseMap();
	}
}
