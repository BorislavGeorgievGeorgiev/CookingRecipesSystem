using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IUserManagerService : ITransientService
	{
		Task<(ApplicationResult Result, string? UserName)> GetUserName(string userId);

		Task<(ApplicationResult Result, string? UserId)> FindUserIdByEmail(string email);

		Task<(ApplicationResult Result, bool IsRightPassowrd)> CheckPassword(string userId, string password);

		Task<ApplicationResult> ChangePassword(
			string userId, string currentPassword, string newPassowrd);

		Task<(ApplicationResult Result, string UserId)> CreateUser(
			string userName, string email, string passwordd);

		Task<ApplicationResult> DeleteUser(string userId);
	}
}
