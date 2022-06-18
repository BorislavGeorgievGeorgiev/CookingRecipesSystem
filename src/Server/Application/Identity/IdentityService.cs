using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Identity
{
	public class IdentityService : IIdentityService
	{
		private readonly IUserManagerService _userManagerService;
		private readonly IJwtService _jwtService;

		public IdentityService(
			IUserManagerService userManagerService,
			IJwtService jwtService)
		{
			this._userManagerService = userManagerService;
			this._jwtService = jwtService;
		}

		public async Task<(ApplicationResult Result, string UserId)> Register(UserRequestModel userRequest)
		{
			var newUser = await this._userManagerService.CreateUser(
				userRequest.UserName, userRequest.Email, userRequest.Password);

			return newUser;
		}

		public async Task<(ApplicationResult Result, UserResponseModel response)> Login(UserRequestModel userRequest)
		{
			var userId = await this._userManagerService.FindByEmailAsync(userRequest.Email);

			if (userId == null)
			{
				throw new ArgumentNullException("Invalid credentials.");
			}

			var isValidPassword = await this._userManagerService.CheckPasswordAsync(userId!, userRequest.Password);

			if (!isValidPassword)
			{
				throw new ArgumentNullException("Invalid passowrd.");
			}

			var token = await this._jwtService.GenerateToken(userId!, userRequest.Email);

			var result = new UserResponseModel(token);

			return (ApplicationResult.Success, result);
		}

		public Task<ApplicationResult> ChangePassword(
			ChangePasswordRequestModel changePasswordRequest)
			=> this._userManagerService.ChangePasswordAsync(
				changePasswordRequest.UserId,
				changePasswordRequest.CurrentPassword,
				changePasswordRequest.NewPassword);
	}
}
