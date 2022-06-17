using CookingRecipesSystem.Infrastructure.Identity;

namespace CookingRecipesSystem.Infrastructure.Services
{
	public interface IJwtService
	{
		Task<string> GenerateToken(ApplicationUser user);
	}
}