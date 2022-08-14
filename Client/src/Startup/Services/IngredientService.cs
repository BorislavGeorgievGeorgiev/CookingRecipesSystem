﻿using CookingRecipesSystem.Startup.Extensions;
using CookingRecipesSystem.Startup.Helpers;
using CookingRecipesSystem.Startup.Models;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IIngredientService
	{
		Task<AppResult> Create(IngredientModel ingredientModel);
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
	}
}
