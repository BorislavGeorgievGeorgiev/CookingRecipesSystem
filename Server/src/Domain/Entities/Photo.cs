using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Photo : AuditableEntity<int>, IAggregateRoot
	{
		private string? _title;
		private byte[]? photoData;

		public Photo(string title, byte[] photoData)
		{
			this.Title = title;
			this.PhotoData = photoData;
		}

		public string Title
		{
			get => this._title!;
			private set
			{
				Guard.IsNotNullOrWhiteSpace(value, nameof(this.Title));
				this._title = value;
			}
		}

		public byte[] PhotoData
		{
			get => this.photoData!;
			private set
			{
				Guard.IsNotNull(value, nameof(this.PhotoData));
				this.photoData = value;
			}
		}
	}
}
