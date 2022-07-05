using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Common;

using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public abstract class ApplicationData<TDbContext, TEntity> : IApplicationData<TEntity>
		where TDbContext : DbContext
		where TEntity : class, IAggregateRoot
	{
		protected ApplicationData(TDbContext dbData) => this.DbData = dbData;

		protected TDbContext DbData { get; }

		protected IQueryable<TEntity> GetAll() => this.DbData.Set<TEntity>();

		protected IQueryable<TEntity> GetAllNoTracking() => this.GetAll().AsNoTracking();

		public Task<int> SaveAsync(
			CancellationToken cancellationToken = new CancellationToken())
			=> this.DbData.SaveChangesAsync(cancellationToken);
	}
}
