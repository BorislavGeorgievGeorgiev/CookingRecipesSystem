
using CookingRecipesSystem.Domain.Common.Constants;
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

				var hasAdminRole = roleManager.Roles.SingleOrDefault(
					r => r.Name == AppConstants.RoleNameAdministrator);

				if (hasAdminRole == null)
				{
					await roleManager.CreateAsync(new IdentityRole(AppConstants.RoleNameAdministrator));
				}

				var hasUser = userManager.Users.SingleOrDefault(u => u.Email == defaultUser.Email);

				if (hasUser == null)
				{
					await userManager.CreateAsync(defaultUser, UserPassword);
					await userManager.AddToRoleAsync(defaultUser, AppConstants.RoleNameAdministrator);
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
