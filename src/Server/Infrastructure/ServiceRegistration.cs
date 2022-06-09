using System.Reflection;

using CookingRecipesSystem.Application;
using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Infrastructure.Identity;
using CookingRecipesSystem.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookingRecipesSystem.Infrastructure
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddInfrastructure(
				this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddDbContext<CookingRecipesSystemDbContext>(options => options
				.UseSqlServer(
					configuration.GetConnectionString(ApplicationConstants.DefaultConnection),
					bilder => bilder.MigrationsAssembly(
						typeof(CookingRecipesSystemDbContext).Assembly.FullName)));

			services.AddConventionalServices(Assembly.GetExecutingAssembly());

			services
				.AddIdentityCore<ApplicationUser>(options =>
				 {
					 options.Password.RequireNonAlphanumeric = false;
					 options.Password.RequireDigit = false;
					 options.Password.RequireUppercase = false;
					 options.Password.RequireLowercase = false;
					 options.Password.RequireUppercase = false;
					 options.Password.RequiredUniqueChars = 0;
					 options.Password.RequiredLength = 3;
				 })
				 .AddEntityFrameworkStores<CookingRecipesSystemDbContext>();



			return services;
		}
	}
}
