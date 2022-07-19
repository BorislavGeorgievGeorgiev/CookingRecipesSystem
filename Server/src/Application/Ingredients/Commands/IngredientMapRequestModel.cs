using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Ingredients.Commands
{
	public class IngredientMapRequestModel : IMapTo<Ingredient>
	{
		public string Name { get; set; }

		public string Description { get; set; }
	}
}
