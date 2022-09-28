using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Entities;
using CookingRecipesSystem.Infrastructure.Persistence;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public class RecipeTaskRepository :
		AppRepository<CookingRecipesSystemDbContext, RecipeTask>, IAppRepository<RecipeTask>
	{
		public RecipeTaskRepository(CookingRecipesSystemDbContext dbContext) : base(dbContext)
		{
		}
	}
}
