using CookingRecipesSystem.Application;

using Microsoft.Extensions.DependencyInjection;

namespace CookingRecipesSystem.Web
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddWebComponents(
			this IServiceCollection services)
			=> services
			.AddHttpContextAccessor()
			.AddConventionalServices(typeof(ServiceRegistration).Assembly);
	}
}
