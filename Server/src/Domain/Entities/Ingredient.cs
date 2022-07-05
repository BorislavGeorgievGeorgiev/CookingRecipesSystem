
using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Ingredient : AuditableEntity<int>
	{
		private string? _name;
		private string? _description;
		private Photo? _ingredientPhoto;
		private Photo? _thumbnailPhoto;

		public Ingredient(string name, string description, string createdBy)
		{
			this.Name = name;
			this.Description = description;
			this.CreatedBy = createdBy;
		}

		public string Name
		{
			get { return this._name!; }
			private set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Name));
				this._name = value;
			}
		}

		public Photo IngredientPhoto
		{
			get => this._ingredientPhoto!;
			set
			{
				Guard.IsNotNull(value, nameof(this.IngredientPhoto));
				this._ingredientPhoto = value;
			}
		}

		public Photo ThumbnailPhoto
		{
			get => this._thumbnailPhoto!;
			set
			{
				Guard.IsNotNull(value, nameof(this.ThumbnailPhoto));
				this._thumbnailPhoto = value;
			}
		}

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
