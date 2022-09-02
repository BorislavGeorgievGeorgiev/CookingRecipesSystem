using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class Photo : AuditableEntity<int>, IAggregateRoot
	{
		public byte[] MainPhoto { get; set; }

		public byte[] CardPhoto { get; set; }

		public byte[] Thumbnail { get; set; }
	}
}
