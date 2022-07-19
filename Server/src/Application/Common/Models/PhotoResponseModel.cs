using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Common.Models
{
	public class PhotoResponseModel : IMapTo<Photo>
	{
		public byte[] MainPhoto { get; set; }

		public byte[] PhonePhoto { get; set; }

		public byte[] Thumbnail { get; set; }
	}
}
