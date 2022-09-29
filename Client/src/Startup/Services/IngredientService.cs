using System.Net;

using CookingRecipesSystem.Startup.Constants;
using CookingRecipesSystem.Startup.Extensions;
using CookingRecipesSystem.Startup.Helpers;
using CookingRecipesSystem.Startup.Models;
using CookingRecipesSystem.Startup.Models.Ingredient;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IIngredientService
	{
		Task<AppResult<EntityKeyResponseModel>> Create(IngredientPostModel ingredientModel);

		Task<AppResult<IngredientGetModel>> GetById(int id);

		Task<AppResult<IEnumerable<IngredientGetModel>>> GetAll();
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

		public async Task<AppResult<EntityKeyResponseModel>> Create(
			IngredientPostModel ingredientModel)
		{
			HttpResponseMessage? response = null;
			AppResult<EntityKeyResponseModel> result;

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
				return AppResult<EntityKeyResponseModel>.Failure(ex.Message);
			}

			if (response.IsSuccessStatusCode == false)
			{
				if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					return AppResult<EntityKeyResponseModel>.Failure(ErrorMessages.NotLogedIn);
				}
			}

			result = await response.JsonDeserializeResponseAsync<EntityKeyResponseModel>();

			return result;
		}

		public async Task<AppResult<IngredientGetModel>> GetById(int id)
		{
			HttpResponseMessage? response = null;
			AppResult<IngredientGetModel> result;

			try
			{
				var uri = _configurationHelper
					.GetRequestUri(IngredientsControllerName, nameof(GetById)) + "/" + id;
				response = await _httpClient.GetAsync(uri);
			}
			catch (Exception ex)
			{
				//TODO: Log exception.
				return AppResult<IngredientGetModel>.Failure(ex.Message);
			}

			result = await response.JsonDeserializeResponseAsync<IngredientGetModel>();

			return result;
		}

		public async Task<AppResult<IEnumerable<IngredientGetModel>>> GetAll()
		{
			HttpResponseMessage? response = null;
			AppResult<IEnumerable<IngredientGetModel>> result = null;

			try
			{
				var uri = _configurationHelper
					.GetRequestUri(IngredientsControllerName, nameof(GetAll));
				response = await _httpClient.GetAsync(uri);
			}
			catch (Exception ex)
			{
				//TODO: Log exception.
				return AppResult<IEnumerable<IngredientGetModel>>.Failure(ex.Message);
			}

			result = await response.JsonDeserializeResponseAsync<IEnumerable<IngredientGetModel>>();

			return result;
		}
	}
}
