using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.Common.ProxyUtilities.Enums;
using System.Text.Json;

namespace Print3dMarketplace.Common.ProxyUtilities;
public static class ProxyServiceExtensions
{
	public static async Task<TResult?> ClientGetAsync<TResult>(
		this IHttpClientFactory clientFactory,
		KnownHttpClients clientName,
		string requestUri) where TResult : class
	{
		try
		{
			var client = clientFactory.CreateClient(clientName.ToString());

			var response = await client.GetAsync(requestUri);
			var content = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};

			if (string.IsNullOrEmpty(content))
				return null;

			var responseDto = JsonSerializer.Deserialize<ResponseDto<TResult>>(content, options);

			if (responseDto == null || !responseDto.IsSuccess)
				return null;

			return responseDto.Result;

		}
		catch (Exception ex)
		{
			return null;
		}
	}
}
