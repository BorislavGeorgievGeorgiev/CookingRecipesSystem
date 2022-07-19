using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Common.Constants;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Recipe : AuditableEntity<int>, IAggregateRoot
	{
		private string? _title;
		private string? _description;

		public string Title
		{
			get { return this._title!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Title));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.RecipeTitleMaxLength, nameof(this.Title));
				this._title = value;
			}
		}

		public string Description
		{
			get { return this._description!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Description));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.RecipeDescriptionMaxLength, nameof(this.Description));
				this._description = value;
			}
		}

		public Photo Photo { get; set; }

		public ICollection<Ingredient> Ingredients { get; } = new HashSet<Ingredient>();

		public ICollection<RecipeTask> RecipeTasks { get; } = new HashSet<RecipeTask>();
	}
}
