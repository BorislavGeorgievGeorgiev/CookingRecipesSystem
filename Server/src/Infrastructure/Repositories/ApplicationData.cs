﻿
using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Common;

using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public abstract class ApplicationData<TDbContext, TEntity> : IApplicationData<TEntity>
		where TDbContext : DbContext
		where TEntity : class, IAggregateRoot, IDeletableEntity
	{
		protected ApplicationData(TDbContext dbContext)
		{
			Guard.IsNotNull(dbContext, nameof(dbContext));
			this._Context = dbContext;
			this._DbSet = dbContext.Set<TEntity>();
		}

		private TDbContext _Context { get; }

		private DbSet<TEntity> _DbSet { get; }

		public async Task<bool> Create(TEntity entity, CancellationToken cancellationToken = default)
		{
			var entry = this._Context.Entry(entity);

			if (entry.State != EntityState.Detached)
			{
				entry.State = EntityState.Added;
			}
			else
			{
				await this._DbSet.AddAsync(entity, cancellationToken);
			}

			return true;
		}

		public bool Update(TEntity entity)
		{
			var entry = this._Context.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				this._DbSet.Attach(entity);
			}

			entry.State = EntityState.Modified;

			return true;
		}

		public bool DeleteNoPermanent(TEntity entity)
		{
			var entry = this._Context.Entry(entity);

			if (entry.State == EntityState.Detached)
			{
				this._DbSet.Attach(entity);
			}

			entry.State = EntityState.Modified;

			entity.IsDeleted = true;

			return true;
		}

		public async Task<TEntity?> GetById<TKey>(TKey id, CancellationToken cancellationToken = default)
		{
			return await this._DbSet.FindAsync(new TKey[] { id }, cancellationToken);
		}

		public IQueryable<TEntity> GetAll()
		{
			return this._DbSet.Where(x => !x.IsDeleted);
		}

		public IQueryable<TEntity> GetAllAsNoTracking()
		{
			return this.GetAll().AsNoTracking();
		}

		public async Task<int> SaveAsync(
			CancellationToken cancellationToken = new CancellationToken())
		{
			return await this._Context.SaveChangesAsync(cancellationToken);
		}
	}
}
