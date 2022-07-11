using System.Reflection;

using CookingRecipesSystem.Application;
using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Domain.Common;
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
					configuration.GetConnectionString(ApplicationConstants.DefaultConnection),
					bilder => bilder.MigrationsAssembly(
						typeof(CookingRecipesSystemDbContext).Assembly.FullName)));

			services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)),
				options => options.BindNonPublicProperties = true);

			services
				.AddIdentity<ApplicationUser, IdentityRole>(options =>
				 {
					 options.User.RequireUniqueEmail = true;
					 options.Password.RequireNonAlphanumeric = ApplicationConstants.PasswordRequireNonAlphanumericValue;
					 options.Password.RequireDigit = ApplicationConstants.PasswordRequireDigitValue;
					 options.Password.RequireUppercase = ApplicationConstants.PasswordRequireUppercaseValue;
					 options.Password.RequiredUniqueChars = ApplicationConstants.PasswordRequiredUniqueCharsValue;
					 options.Password.RequiredLength = ApplicationConstants.PasswordMinLength;
				 })
				 .AddEntityFrameworkStores<CookingRecipesSystemDbContext>();

			services.AddConventionalServices(Assembly.GetExecutingAssembly());
			services.AddRepositories(Assembly.GetExecutingAssembly());

			services.AddTokenAuthentication(configuration);
			services.AddAuthorization();

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
