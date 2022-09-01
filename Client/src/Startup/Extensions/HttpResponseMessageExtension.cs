using System.Text.Json;

using CookingRecipesSystem.Startup.Models;

namespace CookingRecipesSystem.Startup.Extensions
{
	public static class HttpResponseMessageExtension
	{
		public static async Task<AppResult> JsonDeserializeResponseAsync(
			this HttpResponseMessage? response)
		{
			var result = JsonSerializer
				.Deserialize<AppResult>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}

		public static async Task<AppResult<TModel>> JsonDeserializeResponseAsync<TModel>(
			this HttpResponseMessage? response)
			where TModel : class
		{
			var result = JsonSerializer
				.Deserialize<AppResult<TModel>>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
	}
}
