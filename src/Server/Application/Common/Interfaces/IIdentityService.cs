using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IIdentityService : ITransientService
	{
		//Task<ApplicationResult<IApplicationUser>> Register(UserRequestModel userRequest);

		//Task<ApplicationResult<UserResponseModel>> Login(UserRequestModel userRequest);

		Task<ApplicationResult> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
	}
}
