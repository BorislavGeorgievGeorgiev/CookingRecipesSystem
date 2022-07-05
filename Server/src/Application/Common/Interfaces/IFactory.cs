using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IFactory<out TEntity> : ITransientService
		where TEntity : IAggregateRoot
	{
		TEntity Create();
	}
}
