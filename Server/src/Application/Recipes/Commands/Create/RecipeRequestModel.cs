using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.Recipes.Commands.Create
{
	public class RecipeRequestModel : IMapTo<Recipe>
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public IFormFile PhotoFile { get; set; }

		public ICollection<EntityKeyModel> Ingredients { get; set; }

		public ICollection<RecipeTaskRequestModel> RecipeTasks { get; set; }
	}
}
