using Print3dMarketplace.AuthAPI.Contracts.DTOs;

namespace Print3dMarketplace.AuthAPI.Services.Interfaces;

public interface ICreatorService
{
	Task AddCreatorInfo(CreatorRegistrationRequestDto creatorRegistrationRequestDto, Guid userId);
	Task<CreatorDto> GetCreator(Guid userId);
}
