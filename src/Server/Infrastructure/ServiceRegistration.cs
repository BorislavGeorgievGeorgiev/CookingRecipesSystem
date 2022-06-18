﻿using System.Reflection;

using CookingRecipesSystem.Application;
using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Identity;
using CookingRecipesSystem.Domain.Common;
using CookingRecipesSystem.Infrastructure.Common;
using CookingRecipesSystem.Infrastructure.Identity;
using CookingRecipesSystem.Infrastructure.Persistence;
using CookingRecipesSystem.Infrastructure.Services;

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
				.AddTransient<IIdentityService, IdentityService>()
				.AddTransient<IJwtService, JwtService>()
				.AddIdentity<ApplicationUser, IdentityRole>(options =>
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

			services.AddConventionalServices(Assembly.GetExecutingAssembly());

			services.AddTokenAuthentication(configuration);
			services.AddAuthorization();

			return services;
		}
	}
}
