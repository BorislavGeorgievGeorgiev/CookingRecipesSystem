using CookingRecipesSystem.Startup.Models.Photo;

namespace CookingRecipesSystem.Startup.Models.Ingredient
{
  public class IngredientGetModel
  {
    public string Name { get; set; }

    public string Description { get; set; }

    public PhotoGetModel Photo { get; set; }
  }
}
