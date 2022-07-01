using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Recipe : AuditableEntity<int>
	{
		private string? _title;
		private string? _content;

		public Recipe(string title, string content, string createdBy)
		{
			this.Title = title;
			this.Content = content;
			this.CreatedBy = createdBy;
		}

		public string Title
		{
			get { return this._title!; }
			private set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Title));
				this._title = value;
			}
		}

		public string Content
		{
			get { return this._content!; }
			private set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Content));
				this._content = value;
			}
		}
	}
}
