using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.Recipes.Commands.Create
{
	public class RecipeTaskRequestModel : IMapTo<RecipeTask>
	{
		public byte Position { get; set; }

		public string Description { get; set; }

		public IFormFile PhotoFile { get; set; }
	}
}
