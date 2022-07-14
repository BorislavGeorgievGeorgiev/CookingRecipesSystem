using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Entities;
using CookingRecipesSystem.Infrastructure.Persistence;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public class PhotoRepository :
		AppRepository<CookingRecipesSystemDbContext, Photo>, IAppRepository<Photo>
	{
		public PhotoRepository(CookingRecipesSystemDbContext dbContext) :
			base(dbContext)
		{
		}
	}
}
