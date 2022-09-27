using System.Reflection;

using CookingRecipesSystem.Application.Common.Behaviours;
using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Identity.Commands.Register;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace CookingRecipesSystem.Application
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
			=> services
			.AddAutoMapper(Assembly.GetExecutingAssembly())
			.AddMediatR(Assembly.GetExecutingAssembly())
			.AddConventionalServices(Assembly.GetExecutingAssembly())
			.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>(ServiceLifetime.Transient)
			.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

		public static IServiceCollection AddConventionalServices(
			 this IServiceCollection services, Assembly assembly)
		{
			var transientLifetimeInterfaceType = typeof(ITransientService);
			var singletonLifetimeInterfaceType = typeof(ISingletonService);
			var scopedLifetimeInterfaceType = typeof(IScopedService);

			var types = GetConventionalTypes(assembly);

			foreach (var type in types)
			{
				if (transientLifetimeInterfaceType.IsAssignableFrom(type.Service))
				{
					services.AddTransient(type.Service, type.Implementation!);
				}
				else if (singletonLifetimeInterfaceType.IsAssignableFrom(type.Service))
				{
					services.AddSingleton(type.Service, type.Implementation!);
				}
				else if (scopedLifetimeInterfaceType.IsAssignableFrom(type.Service))
				{
					services.AddScoped(type.Service, type.Implementation!);
				}
			}

			return services;
		}

		private static IEnumerable<ServiceTypes> GetConventionalTypes(
			Assembly assembly)
		{
			var types = assembly
				.GetExportedTypes()
				.Where(t => t.IsClass && !t.IsAbstract)
				.Select(t => new ServiceTypes
				{
					Service = t.GetInterface($"I{t.Name}"),
					Implementation = t
				})
				.Where(t => t.Service != null);

			return types;
		}

		private class ServiceTypes
		{
			public Type? Service { get; set; }

			public Type? Implementation { get; set; }
		}
	}
}
