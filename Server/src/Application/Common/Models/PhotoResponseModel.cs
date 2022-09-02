using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Common.Models
{
	public class PhotoResponseModel : IMapFrom<Photo>, IMapTo<Photo>
	{
		public byte[] MainPhoto { get; set; }

		public byte[] CardPhoto { get; set; }

		public byte[] Thumbnail { get; set; }
	}
}
