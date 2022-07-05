using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface ITestEntityRepository : IApplicationData<TestEntity>
	{
		Task<ApplicationResult<IEnumerable<TestEntityResponseModel>>> All(
						CancellationToken cancellationToken = default);
	}
}
