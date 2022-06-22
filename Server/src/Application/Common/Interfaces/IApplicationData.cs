using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IApplicationData : IScopedService
	{
		Task<int> SaveAsync(CancellationToken cancellationToken);
	}
}
