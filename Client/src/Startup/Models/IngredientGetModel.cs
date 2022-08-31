namespace CookingRecipesSystem.Startup.Models
{
	public class IngredientGetModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public PhotoGetModel Photo { get; set; }

		public ICollection<RecipeNavigationGetModel> Recipes { get; set; }
	}
}
