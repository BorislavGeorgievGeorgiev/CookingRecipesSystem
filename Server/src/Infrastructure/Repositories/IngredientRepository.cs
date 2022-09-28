using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Entities;
using CookingRecipesSystem.Infrastructure.Persistence;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public class IngredientRepository :
		AppRepository<CookingRecipesSystemDbContext, Ingredient>, IAppRepository<Ingredient>
	{
		public IngredientRepository(CookingRecipesSystemDbContext dbContext) : base(dbContext)
		{
		}
	}
}
