using CommunityToolkit.Diagnostics;

namespace CookingRecipesSystem.Domain.Entities
{
	public class RecipeTask
	{
		private string? _description;

		public RecipeTask(string description)
			=> this.Description = description;

		public string Description
		{
			get { return this._description!; }
			private set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Description));
				this._description = value;
			}
		}
	}
}
