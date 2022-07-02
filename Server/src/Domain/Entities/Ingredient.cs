
using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Ingredient : AuditableEntity<int>
	{
		private string? _name;
		private string? _description;

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
