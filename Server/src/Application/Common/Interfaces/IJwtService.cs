using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IJwtService : ITransientService
	{
		Task<string> GenerateToken(string userId, string userEmail);
	}
}