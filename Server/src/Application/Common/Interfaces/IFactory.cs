using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IFactory<out TEntity> : ITransientService
		where TEntity : IAggregateRoot
	{
		TEntity Create();
	}
}
