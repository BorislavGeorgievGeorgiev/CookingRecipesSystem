using System.Text.Json;

using CookingRecipesSystem.Startup.Models;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IIngredientService
	{
		Task<AppResult> Create(IngredientModel ingredientModel);
	}

	public class IngredientService : IIngredientService
	{
		private const string IngredientsPath = "/Ingredients/";

		private readonly string? _apiIngredientUri;
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public IngredientService(
			HttpClient httpClient,
			IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
			_apiIngredientUri = _configuration
				.GetSection(nameof(ApiConfig))
				.GetSection(nameof(ApiConfig.ApiUrl)).Value + IngredientsPath; ;
		}

		public async Task<AppResult> Create(IngredientModel ingredientModel)
		{
			HttpResponseMessage? response = null;
			using var content = new MultipartFormDataContent();
			using var stream = ingredientModel.Photo.OpenReadStream(ingredientModel.Photo.Size);

			content.Add(new StringContent(ingredientModel.Name),
				nameof(ingredientModel.Name));
			content.Add(new StringContent(ingredientModel.Description),
				nameof(ingredientModel.Description));
			content.Add(new StreamContent(stream, Convert.ToInt32(ingredientModel.Photo.Size)),
				nameof(ingredientModel.Photo), ingredientModel.Photo.Name);

			try
			{
				response = await _httpClient.PostAsync(GetRequestUri(nameof(Create)), content);
			}
			catch (Exception ex)
			{
				//TODO: Log exception.
				return AppResult.Failure(ex.Message);
			}

			var result = await DeserializeResponseAsync(response);

			return result;
		}

		private string GetRequestUri(string action)
		{
			string uri = _apiIngredientUri + action;
			return uri.ToLower();
		}

		private static async Task<AppResult> DeserializeResponseAsync(
			HttpResponseMessage? response)
		{
			var result = JsonSerializer
				.Deserialize<AppResult>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}

		private static async Task<AppResult<TModel>> DeserializeResponseAsync<TModel>(
			HttpResponseMessage? response)
			where TModel : class
		{
			var result = JsonSerializer
				.Deserialize<AppResult<TModel>>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
	}
}
