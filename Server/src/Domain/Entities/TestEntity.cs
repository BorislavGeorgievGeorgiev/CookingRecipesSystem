using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.Entities
{
	public class TestEntity : AuditableEntity<int>, IAggregateRoot
	{
		public string? Text { get; set; }
	}
}
