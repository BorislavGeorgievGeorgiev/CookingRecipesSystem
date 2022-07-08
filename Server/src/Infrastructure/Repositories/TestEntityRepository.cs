
using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Entities;
using CookingRecipesSystem.Infrastructure.Persistence;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public class TestEntityRepository
		: AppRepository<CookingRecipesSystemDbContext, TestEntity>, IAppRepository<TestEntity>
	{
		public TestEntityRepository(CookingRecipesSystemDbContext dbData)
			: base(dbData)
		{
		}
	}
}
