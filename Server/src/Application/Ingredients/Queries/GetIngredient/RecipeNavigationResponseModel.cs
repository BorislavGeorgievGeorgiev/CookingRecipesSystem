using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient
{
  public class RecipeNavigationResponseModel : IMapFrom<Recipe>
  {
    public int Id { get; set; }

    public string Title { get; set; }
  }
}
