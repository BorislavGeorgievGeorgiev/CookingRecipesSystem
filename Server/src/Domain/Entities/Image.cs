using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Image : AuditableEntity<int>, IAggregateRoot
	{
		public Image(byte[] original, byte[] thumbnail)
		{
			this.Original = original;
			this.Thumbnail = thumbnail;
		}

		public byte[] Original { get; }

		public byte[] Thumbnail { get; }
	}
}
