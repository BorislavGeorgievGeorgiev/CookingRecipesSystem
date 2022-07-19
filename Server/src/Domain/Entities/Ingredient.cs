using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Common.Constants;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Ingredient : AuditableEntity<int>, IAggregateRoot
	{
		private string? _name;
		private string? _description;

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

		public Photo Photo { get; set; }

		public ICollection<Recipe> Recipes { get; } = new HashSet<Recipe>();
	}
}
