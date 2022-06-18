using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IIdentityService
	{
		Task<(ApplicationResult Result, string UserId)> Register(UserRequestModel userRequest);

		Task<(ApplicationResult Result, UserResponseModel response)> Login(UserRequestModel userRequest);

		Task<ApplicationResult> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
	}
}
