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

				var defaultUser = new ApplicationUser
				{
					UserName = "admin@test.com",
					Email = "admin@test.com"
				};

				if (userManager.Users.All(u => u.Id != defaultUser.Id))
				{
					await userManager.CreateAsync(defaultUser, "test");
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
