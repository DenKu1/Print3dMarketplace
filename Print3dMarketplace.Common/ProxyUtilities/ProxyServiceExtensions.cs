using Print3dMarketplace.Common.DTOs;
using Print3dMarketplace.Common.ProxyUtilities.Enums;
using System.Text;
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
			using (var client = clientFactory.CreateClient(clientName.ToString()))
			{
				var response = await client.GetAsync(requestUri);

				return await DeserializeResponseAsync<TResult>(response);
			}

		}
		catch (Exception ex)
		{
			return null;
		}
	}

	public static async Task<TResult?> ClientPostAsync<TResult, TRequest>(
		this IHttpClientFactory clientFactory,
		KnownHttpClients clientName,
		string requestUri,
		TRequest model) where TResult : class
						where TRequest: class
	{
		ArgumentNullException.ThrowIfNull(model);

		using (var client = clientFactory.CreateClient(clientName.ToString()))
		{
			var response = await client.PostAsync
				(
					requestUri,
					new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
				);

			return await DeserializeResponseAsync<TResult>(response);
		}
	}

	private static async Task<TResult?> DeserializeResponseAsync<TResult>(HttpResponseMessage response) where TResult : class
	{
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
}
