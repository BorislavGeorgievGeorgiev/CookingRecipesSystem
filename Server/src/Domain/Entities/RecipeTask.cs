using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Common.Constants;

namespace CookingRecipesSystem.Domain.Entities
{
	public class RecipeTask : AuditableEntity<int>, IAggregateRoot
	{
		private string? _description;

		public RecipeTask(byte position, string description, string createdBy, Image image) :
			this(position, description, createdBy)
		{
			this.Description = description;
			this.CreatedBy = createdBy;
			this.Position = position;
			this.Image = image;
		}

		private RecipeTask(byte position, string description, string createdBy)
		{
			this.Description = description;
			this.CreatedBy = createdBy;
			this.Position = position;
		}

		public byte Position { get; set; }

		public string Description
		{
			get { return this._description!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Description));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.RecipeTaskDescriptionMaxLength, nameof(this.Description));
				this._description = value;
			}
		}

		public Image Image { get; set; }
	}
}
