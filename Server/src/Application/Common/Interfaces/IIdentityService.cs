using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IIdentityService : ITransientService
	{
		Task<ApplicationResult<UserIdResponseModel>> Register(UserRegisterRequestModel userRequest);

		Task<ApplicationResult<UserTokenResponseModel>> Login(UserLoginRequestModel userRequest);

		Task<ApplicationResult> ChangePassword(ChangePasswordRequestModel changePasswordRequest);

		Task<ApplicationResult> DeleteUser(UserIdRequestModel userIdRequest);

		Task<ApplicationResult<UserNameResponseModel>> GetUserName(UserIdRequestModel userIdRequest);

		Task<ApplicationResult<UserIdResponseModel>> FindUserIdByEmail(UserEmailRequestModel emailRequest);
	}
}
