using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Application.Common.Interfaces.Factories
{
	public interface IFactory<out TEntity> : ITransientService
		where TEntity : class, IAggregateRoot
	{
		TEntity Create();
	}
}
