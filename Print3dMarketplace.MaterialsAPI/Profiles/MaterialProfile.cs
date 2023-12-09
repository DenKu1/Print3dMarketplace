using AutoMapper;
using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;
using Print3dMarketplace.MaterialsAPI.Entities;

namespace Print3dMarketplace.MaterialsAPI.Profiles;

public class MaterialProfile : Profile
{
	public MaterialProfile()
	{
		CreateMap<MaterialDto, Material>();
	}
}
