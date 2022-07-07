using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class RecipeTask : AuditableEntity<int>, IAggregateRoot
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

		public ICollection<Photo> Photos { get; } = new List<Photo>();
	}
}
