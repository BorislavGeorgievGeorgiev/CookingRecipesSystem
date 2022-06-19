using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IIdentityService
	{
		Task<(ApplicationResult Result, UserIdResponseModel Response)> Register(UserRequestModel userRequest);

		Task<(ApplicationResult Result, UserTokenResponseModel Response)> Login(UserRequestModel userRequest);

		Task<ApplicationResult> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
	}
}
