using System.Reflection;

using CookingRecipesSystem.Application;
using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Infrastructure.Common;
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

			//services
			//	.Scan(scan => scan
			//	.FromAssemblies(Assembly.GetExecutingAssembly())
			//	.AddClasses(classes => classes
			//	.AssignableTo(typeof(IApplicationData<>)))
			//	.AsImplementedInterfaces()
			//	.WithTransientLifetime());

			services.AddTokenAuthentication(configuration);
			services.AddAuthorization();

			return services;
		}
	}
}
