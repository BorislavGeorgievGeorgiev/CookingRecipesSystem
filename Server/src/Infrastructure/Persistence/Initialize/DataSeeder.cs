
using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;
using CookingRecipesSystem.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CookingRecipesSystem.Infrastructure.Persistence.Initialize
{
	public static class DataSeeder
	{
		public static async Task SeedAsync(IServiceProvider serviceProvider)
		{
			const string UserEmail = "admin@test.com";
			const string UserPassword = "test";

			using var context = serviceProvider.GetRequiredService<CookingRecipesSystemDbContext>();

			try
			{
				context.Database.Migrate();

				var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
				var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				var defaultUser = new ApplicationUser
				{
					UserName = UserEmail,
					Email = UserEmail
				};

				var hasAdminRole = roleManager.Roles.All(
					r => r.Name == AppConstants.RoleNameAdministrator);

				if (!hasAdminRole)
				{
					await roleManager.CreateAsync(new IdentityRole(AppConstants.RoleNameAdministrator));
				}

				var hasUser = userManager.Users.All(u => u.Email == defaultUser.Email);

				if (!hasUser)
				{
					await userManager.CreateAsync(defaultUser, UserPassword);
					await userManager.AddToRoleAsync(defaultUser, AppConstants.RoleNameAdministrator);
				}

				if (!context.TestEntities.Any())
				{
					var user = userManager.Users.FirstOrDefault(u => u.Email == defaultUser.Email);

					Guard.IsNotNull(user, nameof(user));

					for (int i = 0; i < 10; i++)
					{
						var testEntity = new TestEntity
						{
							Text = i + " : Test String.",
							CreatedBy = user.Id,
							CreatedOn = DateTime.UtcNow
						};

						context.TestEntities.Add(testEntity);
					}
				}

				await context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				//logger.LogError(ex, "An error occurred while migrating or seeding the database.");
				Console.WriteLine(ex.Message);
			}

		}
	}
}
