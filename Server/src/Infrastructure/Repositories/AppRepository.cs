using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Common;

using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public abstract class AppRepository<TDbContext, TEntity> : IAppRepository<TEntity>
		where TDbContext : DbContext
		where TEntity : class, IAggregateRoot, IDeletableEntity
	{
		protected AppRepository(TDbContext dbContext)
		{
			Guard.IsNotNull(dbContext, nameof(dbContext));
			Context = dbContext;
			DbSet = dbContext.Set<TEntity>();
		}

		private TDbContext Context { get; }

		private DbSet<TEntity> DbSet { get; }

		public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
		{
			var entry = Context.Entry(entity);

			if (entry.State != EntityState.Detached)
			{
				entry.State = EntityState.Added;
			}
			else
			{
				await DbSet.AddAsync(entity, cancellationToken);
			}

			return entry.Entity;
		}

		public async Task<bool> Update(
			TEntity entity, CancellationToken cancellationToken = default)
		{
			var entry = Context.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				DbSet.Attach(entity);
			}

			entry.State = EntityState.Modified;

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteNoPermanent(
			TEntity entity, CancellationToken cancellationToken = default)
		{
			entity.IsDeleted = true;

			return await Update(entity, cancellationToken);
		}

		public IQueryable<TEntity> GetAll(string? include = default)
		{
			if (include == null)
			{
				return DbSet.Where(x => x.IsDeleted == false);
			}
			else
			{
				return DbSet.Where(x => x.IsDeleted == false).Include(include);
			}
		}

		public IQueryable<TEntity> GetAllAsNoTracking(string? include = default)
		{
			return GetAll(include).AsNoTracking();
		}

		public async Task<int> SaveAsync(
			CancellationToken cancellationToken = new CancellationToken())
		{
			return await Context.SaveChangesAsync(cancellationToken);
		}
	}
}
