using Print3dMarketplace.MaterialsAPI.Contracts.DTOs;
using Print3dMarketplace.PrintRequestsAPI.ProxyServices.Interfaces;
using Print3dMarketplace.Common.ProxyUtilities;
using Print3dMarketplace.MaterialsAPI.Contracts.Integration;
using Print3dMarketplace.Common.ProxyUtilities.Enums;

namespace Print3dMarketplace.PrintRequestsAPI.ProxyServices;

public class MaterialProxyService : IMaterialProxyService
{
	private readonly IHttpClientFactory _httpClientFactory;

	public MaterialProxyService(IHttpClientFactory clientFactory)
	{
		_httpClientFactory = clientFactory;
	}

	public async Task<IEnumerable<MaterialDto>> GetAllCreatorMaterials(Guid userId)
	{
		var requestUrl = KnownMaterialEndpoints.GetAllCreatorMaterials + $"/{userId}";

		var materials = await _httpClientFactory.ClientGetAsync<IEnumerable<MaterialDto>>(KnownHttpClients.MaterialsAPI, requestUrl);

		return materials ?? Enumerable.Empty<MaterialDto>();
	}
}
