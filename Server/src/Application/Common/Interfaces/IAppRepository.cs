using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IAppRepository<TEntity> : ITransientService
		where TEntity : class, IAggregateRoot, IDeletableEntity
	{
		IQueryable<TEntity> GetAll(string? include = default);

		IQueryable<TEntity> GetAllAsNoTracking(string? include = default);

		Task<TEntity> Create(TEntity entity,
						CancellationToken cancellationToken = default);

		Task<bool> Update(TEntity entity,
						CancellationToken cancellationToken = default);

		Task<bool> DeleteNoPermanent(TEntity entity,
						CancellationToken cancellationToken = default);

		Task<int> SaveAsync(CancellationToken cancellationToken);
	}
}
