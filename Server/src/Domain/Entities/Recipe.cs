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
			get { return _title!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(Title));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.RecipeTitleMaxLength, nameof(Title));
				_title = value;
			}
		}

		public string Description
		{
			get { return _description!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(Description));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.RecipeDescriptionMaxLength, nameof(Description));
				_description = value;
			}
		}

		public Photo Photo { get; set; }

		public ICollection<Ingredient> Ingredients { get; } = new HashSet<Ingredient>();

		public ICollection<RecipeTask> RecipeTasks { get; } = new HashSet<RecipeTask>();
	}
}
