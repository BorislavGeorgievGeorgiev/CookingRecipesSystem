
using CookingRecipesSystem.Domain.Common;
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
			using var context = serviceProvider.GetRequiredService<CookingRecipesSystemDbContext>();

			try
			{
				context.Database.Migrate();

				var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
				var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				var defaultUser = new ApplicationUser
				{
					UserName = "admin@test.com",
					Email = "admin@test.com"
				};

				var hasAdminRole = roleManager.Roles.All(
					r => r.Name == ApplicationConstants.RoleNameAdministrator);

				if (!hasAdminRole)
				{
					await roleManager.CreateAsync(new IdentityRole(ApplicationConstants.RoleNameAdministrator));
				}

				var hasUser = userManager.Users.All(u => u.Email == defaultUser.Email);

				if (!hasUser)
				{
					await userManager.CreateAsync(defaultUser, "test");
					await userManager.AddToRoleAsync(defaultUser, ApplicationConstants.RoleNameAdministrator);
				}

				if (!context.TestEntities.Any())
				{
					var user = userManager.Users.FirstOrDefault(u => u.Email == defaultUser.Email);

					for (int i = 0; i < 10; i++)
					{
						var testEntity = new TestEntity();
						testEntity.Text = i + " : Test String.";
						testEntity.CreatedBy = user.Id;
						testEntity.CreatedOn = DateTime.UtcNow;

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
