using AutoMapper;
using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;
using Print3dMarketplace.SchemeStorageAPI.Models;

namespace Print3dMarketplace.SchemeStorageAPI.Profiles;

public class StlRequestProfile : Profile
{
	public StlRequestProfile()
	{
		CreateMap<Scheme, StlSchemeRequestDTO>();
	}
}
