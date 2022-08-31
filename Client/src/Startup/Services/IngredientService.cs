using CookingRecipesSystem.Startup.Extensions;
using CookingRecipesSystem.Startup.Helpers;
using CookingRecipesSystem.Startup.Models;
using CookingRecipesSystem.Startup.Models.Ingredient;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IIngredientService
	{
		Task<AppResult> Create(IngredientPostModel ingredientModel);

		Task<AppResult<IngredientGetModel>> GetById(int id);
	}

	public class IngredientService : IIngredientService
	{
		private const string IngredientsControllerName = "Ingredients";

		private readonly HttpClient _httpClient;
		private readonly ConfigurationHelper _configurationHelper;

		public IngredientService(
			HttpClient httpClient,
			ConfigurationHelper configurationHelper)
		{
			_httpClient = httpClient;
			_configurationHelper = configurationHelper;
		}

		public async Task<AppResult> Create(IngredientPostModel ingredientModel)
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
				var uri = _configurationHelper
					.GetRequestUri(IngredientsControllerName, nameof(Create));
				response = await _httpClient.PostAsync(uri, content);
			}
			catch (Exception ex)
			{
				//TODO: Log exception.
				return AppResult.Failure(ex.Message);
			}

			var result = await response.DeserializeResponseAsync();

			return result;
		}

		public async Task<AppResult<IngredientGetModel>> GetById(int id)
		{
			var uri = _configurationHelper
					.GetRequestUri(IngredientsControllerName, nameof(GetById)) + "/" + id;

			try
			{
				var ingredient = await _httpClient.GetAsync(uri);
			}
			catch (Exception ex)
			{
				//TODO: Log exception.
				return AppResult<IngredientGetModel>.Failure(ex.Message);
			}

			throw new NotImplementedException();
		}
	}
}
