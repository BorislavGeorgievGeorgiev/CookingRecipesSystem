using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient
{
  public class IngredientRecipeNavigationResponseModel : IMapFrom<Recipe>
  {
    public int Id { get; set; }

    public string Title { get; set; }
  }
}
