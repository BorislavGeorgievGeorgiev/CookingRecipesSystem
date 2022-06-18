using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IUserManagerService : ITransientService
	{
		Task<string?> GetUserName(string userId);

		Task<string?> FindByEmailAsync(string email);

		Task<bool> CheckPasswordAsync(string userId, string password);

		Task<ApplicationResult> ChangePasswordAsync(
			string userId, string currentPassword, string newPassowrd);

		Task<(ApplicationResult Result, string UserId)> CreateUser(
			string userName, string email, string passwordd);

		Task<ApplicationResult> DeleteUser(string userId);
	}
}
