using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Identity.Queries.AllTestEntity
{
	public class TestEntityResponseModel : IMapFrom<TestEntity>
	{
		public string? Text { get; set; }
	}
}
