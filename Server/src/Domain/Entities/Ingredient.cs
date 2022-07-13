using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Common.Constants;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Ingredient : AuditableEntity<int>, IAggregateRoot
	{
		private string? _name;
		private string? _description;

		public Ingredient(string name, string description, string createdBy, Image image) :
			this(name, description, createdBy)
		{
			this.Image = image;
		}

		private Ingredient(string name, string description, string createdBy)
		{
			// Entity Framework cannot bind value object in entity constructor
			// https://stackoverflow.com/questions/55749717/entity-framework-cannot-bind-value-object-in-entity-constructor
			this.Name = name;
			this.Description = description;
			this.CreatedBy = createdBy;
		}

		public string Name
		{
			get { return this._name!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Name));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.IngredientNameMaxLength, nameof(this.Name));
				this._name = value;
			}
		}

		public string Description
		{
			get { return this._description!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Description));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.IngredientDescriptionMaxLength, nameof(this.Description));
				this._description = value;
			}
		}

		public Image Image { get; set; }

		public ICollection<Recipe> Recipes { get; } = new HashSet<Recipe>();
	}
}
