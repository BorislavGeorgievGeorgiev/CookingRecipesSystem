using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Entities;
using CookingRecipesSystem.Infrastructure.Persistence;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public class RecipeRepository :
		AppRepository<CookingRecipesSystemDbContext, Recipe>, IAppRepository<Recipe>
	{
		public RecipeRepository(CookingRecipesSystemDbContext dbContext) : base(dbContext)
		{
		}
	}
}
