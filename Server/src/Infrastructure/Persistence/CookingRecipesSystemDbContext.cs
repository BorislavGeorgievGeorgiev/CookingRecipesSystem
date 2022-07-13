using System.Reflection;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Domain.Entities;
using CookingRecipesSystem.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Persistence
{
	public class CookingRecipesSystemDbContext :
		IdentityDbContext<ApplicationUser>
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTimeService _dateTimeService;

		public CookingRecipesSystemDbContext(
			DbContextOptions<CookingRecipesSystemDbContext> options,
			ICurrentUserService currentUserService, IDateTimeService dateTimeService)
			: base(options)
		{
			this._currentUserService = currentUserService;
			this._dateTimeService = dateTimeService;
		}

		public DbSet<TestEntity> TestEntities { get; set; }

		public DbSet<Recipe> Recipes { get; set; }

		public DbSet<Ingredient> Ingredients { get; set; }

		public DbSet<RecipeTask> RecipeTasks { get; set; }

		public DbSet<Photo> Photos { get; set; }

		public override Task<int> SaveChangesAsync(
			CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in this.ChangeTracker.Entries<IAuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy ??= this._currentUserService.GetUserId!;
						entry.Entity.CreatedOn = this._dateTimeService.Now;
						break;
					case EntityState.Modified:
						entry.Entity.ModifiedBy = this._currentUserService.GetUserId!;
						entry.Entity.ModifiedOn = this._dateTimeService.Now;
						entry.Entity.DeletedOn = entry.Entity.IsDeleted ? this._dateTimeService.Now : null;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}
	}
}
