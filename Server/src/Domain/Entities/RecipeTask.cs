using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Common.Constants;

namespace CookingRecipesSystem.Domain.Entities
{
	public class RecipeTask : AuditableEntity<int>, IAggregateRoot
	{
		private string? _description;

		public byte Position { get; set; }

		public string Description
		{
			get { return _description!; }
			set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(Description));
				Guard.HasSizeLessThanOrEqualTo(
					value, EntityConstants.RecipeTaskDescriptionMaxLength, nameof(Description));
				_description = value;
			}
		}

		public int? PhotoId { get; set; }

		public Photo Photo { get; set; }
	}
}
