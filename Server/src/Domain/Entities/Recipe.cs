using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Common.Constants;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Recipe : AuditableEntity<int>, IAggregateRoot
	{
		private string? _title;
		private string? _description;

		public Recipe(string title, string description, string createdBy, Image image) :
			this(title, description, createdBy)
		{
			this.Title = title;
			this.Description = description;
			this.CreatedBy = createdBy;
			this.Image = image;
		}

		private Recipe(string title, string description, string createdBy)
		{
			// Entity Framework cannot bind value object in entity constructor
			// https://stackoverflow.com/questions/55749717/entity-framework-cannot-bind-value-object-in-entity-constructor
			this.Title = title;
			this.Description = description;
			this.CreatedBy = createdBy;
		}

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

		public Image Image { get; set; }

		public ICollection<Ingredient> Ingredients { get; } = new HashSet<Ingredient>();

		public ICollection<RecipeTask> RecipeTasks { get; } = new HashSet<RecipeTask>();
	}
}
