using AutoMapper;
using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;
using Print3dMarketplace.MaterialsAPI.Entities;

namespace Print3dMarketplace.MaterialsAPI.Profiles;

public class TemplateMaterialProfile : Profile
{
	public TemplateMaterialProfile()
	{
		CreateMap<TemplateMaterial, TemplateMaterialDto>();
	}
}
