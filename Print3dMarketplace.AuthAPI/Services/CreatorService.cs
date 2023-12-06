using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Print3dMarketplace.AuthAPI.Contracts.DTOs;
using Print3dMarketplace.AuthAPI.EF;
using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.AuthAPI.Services.Interfaces;
using System.Security.Claims;

namespace Print3dMarketplace.AuthAPI.Services;

public class CreatorService : ICreatorService
{
	private readonly IMapper _mapper;

	private readonly AuthDbContext _context;

	public CreatorService(
		IMapper mapper,
		AuthDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task AddCreatorInfo(CreatorRegistrationRequestDto creatorRegistrationRequestDto, Guid userId)
	{
		var creator = _mapper.Map<Creator>(creatorRegistrationRequestDto);
		creator.ApplicationUserId = userId;

		await _context.Set<Creator>().AddAsync(creator);
		await _context.SaveChangesAsync();
	}

	public async Task<CreatorDto> GetCreator(Guid userId)
	{
		var creator = await _context.Set<Creator>().FirstOrDefaultAsync(x => x.ApplicationUserId == userId);
		return _mapper.Map<CreatorDto>(creator);
	}

	public async Task<bool> UpdateCreator(CreatorDto creatorDto, Guid userId)
	{
		var creator = await _context.Set<Creator>().FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

		_mapper.Map(creatorDto, creator);

		return await _context.SaveChangesAsync() > 0;
	}
}
