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
			get { return _name!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(Name));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.IngredientNameMaxLength, nameof(Name));
				_name = value;
			}
		}

		public string Description
		{
			get { return _description!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(Description));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.IngredientDescriptionMaxLength, nameof(Description));
				_description = value;
			}
		}

		public Photo Photo { get; set; }

		public ICollection<Recipe> Recipes { get; } = new HashSet<Recipe>();
	}
}
