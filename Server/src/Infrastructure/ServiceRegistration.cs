using System.Reflection;

using CookingRecipesSystem.Application;
using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Infrastructure.Common;
using CookingRecipesSystem.Infrastructure.Common.Extensions;
using CookingRecipesSystem.Infrastructure.Identity;
using CookingRecipesSystem.Infrastructure.Persistence;

using Microsoft.AspNetCore.Identity;
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
					configuration.GetConnectionString(AppConstants.DefaultConnection),
					bilder => bilder.MigrationsAssembly(
						typeof(CookingRecipesSystemDbContext).Assembly.FullName)));

			services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)),
				options => options.BindNonPublicProperties = true);

			services
				.AddIdentity<ApplicationUser, IdentityRole>(options =>
				 {
					 options.User.RequireUniqueEmail = true;
					 options.Password.RequireNonAlphanumeric = AppConstants.PasswordRequireNonAlphanumericValue;
					 options.Password.RequireDigit = AppConstants.PasswordRequireDigitValue;
					 options.Password.RequireUppercase = AppConstants.PasswordRequireUppercaseValue;
					 options.Password.RequiredUniqueChars = AppConstants.PasswordRequiredUniqueCharsValue;
					 options.Password.RequiredLength = AppConstants.PasswordMinLength;
				 })
				 .AddEntityFrameworkStores<CookingRecipesSystemDbContext>();

			services.AddConventionalServices(Assembly.GetExecutingAssembly());
			services.AddRepositories(Assembly.GetExecutingAssembly());

			services.AddTokenAuthentication(configuration);

			return services;
		}

		internal static IServiceCollection AddRepositories(
			this IServiceCollection services, Assembly assembly)
			=> services
			.Scan(scan => scan
			.FromAssemblies(assembly)
			.AddClasses(classes => classes.AssignableTo(typeof(IAppRepository<>)))
			.AsImplementedInterfaces()
			.WithTransientLifetime());
	}
}
