using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Common.Models
{
	public class PhotoResponseModel : IMapTo<Photo>
	{
		public PhotoResponseModel(byte[] mainPhoto, byte[] phonePhoto, byte[] thumbnail)
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
