using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Ingredients.Commands
{
	public class IngredientRequestModel : IMapTo<Ingredient>
	{
		public IngredientRequestModel(string name, string description, PhotoRequestModel photo)
		{
			this.Name = name;
			this.Description = description;
			this.Photo = photo;
		}

		public string Name { get; set; }

		public string Description { get; set; }

		public PhotoRequestModel Photo { get; set; }
	}
}
