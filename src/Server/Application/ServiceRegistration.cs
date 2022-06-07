using System.Reflection;

using CookingRecipesSystem.Application.Common.Behaviours;
using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;

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
			.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

		public static IServiceCollection AddConventionalServices(
			 this IServiceCollection services, Assembly assembly)
		{
			var transientLifetimeInterfaceType = typeof(ITransientService);
			var singletonLifetimeInterfaceType = typeof(ISingletonService);
			var scopedLifetimeInterfaceType = typeof(IScopedService);

			var types = assembly
				.GetExportedTypes()
				.Where(t => t.IsClass && !t.IsAbstract)
				.Select(t => new
				{
					Service = t.GetInterface($"I{t.Name}"),
					Implementation = t
				})
				.Where(t => t.Service != null);

			foreach (var type in types)
			{
				if (transientLifetimeInterfaceType.IsAssignableFrom(type.Service))
				{
					services.AddTransient(type.Service, type.Implementation);
				}
				else if (singletonLifetimeInterfaceType.IsAssignableFrom(type.Service))
				{
					services.AddSingleton(type.Service, type.Implementation);
				}
				else if (scopedLifetimeInterfaceType.IsAssignableFrom(type.Service))
				{
					services.AddScoped(type.Service, type.Implementation);
				}
			}

			return services;
		}
	}
}
