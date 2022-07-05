using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Common.Models
{
	public class TestEntityResponseModel : IMapFrom<TestEntity>
	{
		public string? Text { get; set; }
	}
}
