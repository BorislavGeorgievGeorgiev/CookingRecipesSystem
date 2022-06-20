using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IIdentityService
	{
		Task<(ApplicationResult Result, UserIdResponseModel Response)> Register(UserRegisterRequestModel userRequest);

		Task<(ApplicationResult Result, UserTokenResponseModel Response)> Login(UserLoginRequestModel userRequest);

		Task<ApplicationResult> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
	}
}
