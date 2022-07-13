using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Photo : AuditableEntity<int>, IAggregateRoot
	{
		public Photo(byte[] original, byte[] thumbnail)
		{
			this.Original = original;
			this.Thumbnail = thumbnail;
		}

		public byte[] Original { get; }

		public byte[] Thumbnail { get; }
	}
}
