using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient
{
    public class IngredientRecipeResponseModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
