using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface ICurrentUserService : IScopedService
	{
		string GetUserId { get; }

		bool IsAuthenticated { get; }
	}
}
