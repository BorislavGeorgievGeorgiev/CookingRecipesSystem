using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IIdentityService
	{
		Task<ApplicationResult<UserIdResponseModel>> Register(UserRegisterRequestModel userRequest);

		Task<ApplicationResult<UserTokenResponseModel>> Login(UserLoginRequestModel userRequest);

		Task<ApplicationResult> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
	}
}
