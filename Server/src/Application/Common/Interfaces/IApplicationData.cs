using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IApplicationData<in TEntity> : ITransientService
		where TEntity : class, IAggregateRoot
	{
		Task<int> SaveAsync(CancellationToken cancellationToken);
	}
}
