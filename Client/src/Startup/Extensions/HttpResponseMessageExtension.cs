using System.Text.Json;

using CookingRecipesSystem.Startup.Models;

namespace CookingRecipesSystem.Startup.Extensions
{
	public static class HttpResponseMessageExtension
	{
		public static async Task<AppResult> JsonDeserializeResponseAsync(
			this HttpResponseMessage? response)
		{
			var json = await response.Content.ReadAsStringAsync();

			var result = JsonSerializer
				.Deserialize<AppResult>(json,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}

		public static async Task<AppResult<TModel>> JsonDeserializeResponseAsync<TModel>(
			this HttpResponseMessage? response)
			where TModel : class
		{
			var json = await response.Content.ReadAsStringAsync();

			var result = JsonSerializer
				.Deserialize<AppResult<TModel>>(json,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
	}
}
