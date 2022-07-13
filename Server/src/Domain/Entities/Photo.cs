using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Photo : AuditableEntity<int>, IAggregateRoot
	{
		public Photo(byte[] mainPhoto, byte[] phonePhoto, byte[] thumbnail)
		{
			this.MainPhoto = mainPhoto;
			this.PhonePhoto = phonePhoto;
			this.Thumbnail = thumbnail;
		}

		public byte[] MainPhoto { get; }

		public byte[] PhonePhoto { get; }

		public byte[] Thumbnail { get; }
	}
}
