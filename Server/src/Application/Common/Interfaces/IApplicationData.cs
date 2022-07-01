
using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IApplicationData : IScopedService
	{
		//IDbSet<Recipe> Recipes { get; set; }

		Task<int> SaveAsync(CancellationToken cancellationToken);
	}
}
