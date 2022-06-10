using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;

namespace CookingRecipesSystem.Infrastructure.Services
{
	public interface IJwtService : ITransientService
	{
		Task<string> GenerateToken(IApplicationUser user);
	}
}