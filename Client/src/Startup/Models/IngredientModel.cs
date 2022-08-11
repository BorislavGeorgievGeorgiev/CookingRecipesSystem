using Microsoft.AspNetCore.Components.Forms;

namespace CookingRecipesSystem.Startup.Models
{
	public class IngredientModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public IBrowserFile Photo { get; set; }
	}
}
