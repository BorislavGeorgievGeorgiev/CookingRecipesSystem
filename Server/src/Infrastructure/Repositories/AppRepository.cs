
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
			this.Context = dbContext;
			this.DbSet = dbContext.Set<TEntity>();
		}

		private TDbContext Context { get; }

		private DbSet<TEntity> DbSet { get; }

		public async Task<bool> Create(TEntity entity, CancellationToken cancellationToken = default)
		{
			var entry = this.Context.Entry(entity);

			if (entry.State != EntityState.Detached)
			{
				entry.State = EntityState.Added;
			}
			else
			{
				await this.DbSet.AddAsync(entity, cancellationToken);
			}

			return true;
		}

		public async Task<bool> Update(TEntity entity)
		{
			var entry = this.Context.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				this.DbSet.Attach(entity);
			}

			entry.State = EntityState.Modified;

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteNoPermanent(TEntity entity)
		{
			await this.Update(entity);

			entity.IsDeleted = true;

			return true;
		}

		public async Task<TEntity?> GetById<TKey>(TKey id, CancellationToken cancellationToken = default)
		{
			return await this.DbSet.FindAsync(new object?[] { new TKey[] { id } }, cancellationToken: cancellationToken);
		}

		public IQueryable<TEntity> GetAll()
		{
			return this.DbSet.Where(x => !x.IsDeleted);
		}

		public IQueryable<TEntity> GetAllAsNoTracking()
		{
			return this.GetAll().AsNoTracking();
		}

		public async Task<int> SaveAsync(
			CancellationToken cancellationToken = new CancellationToken())
		{
			return await this.Context.SaveChangesAsync(cancellationToken);
		}
	}
}
