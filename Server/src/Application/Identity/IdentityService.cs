using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common;

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

		public async Task<ApplicationResult<UserIdResponseModel>> Register(
			UserRegisterRequestModel userRequest)
			=> await this._userManagerService.CreateUser(
				userRequest.UserName,
				userRequest.Email,
				userRequest.Password);

		public async Task<ApplicationResult<UserTokenResponseModel>> Login(
			UserLoginRequestModel userRequest)
		{
			var resultUserId = await this._userManagerService
				.FindUserIdByEmail(userRequest.Email);

			if (!resultUserId.Succeeded)
			{
				return ApplicationResult<UserTokenResponseModel>.Failure(ExceptionMessages.InvalidCredentials);
			}

			var userId = resultUserId.Response.UserId;

			var resultCheckPassword = await this._userManagerService.CheckPassword(
				userId, userRequest.Password);

			if (!resultCheckPassword.Succeeded)
			{
				return ApplicationResult<UserTokenResponseModel>.Failure(ExceptionMessages.InvalidCredentials);
			}

			var token = await this._jwtService.GenerateToken(userId, userRequest.Email);

			var response = new UserTokenResponseModel(token);

			return ApplicationResult<UserTokenResponseModel>.Success(response);
		}

		public async Task<ApplicationResult> ChangePassword(
			ChangePasswordRequestModel changePasswordRequest)
			=> await this._userManagerService.ChangePassword(
				changePasswordRequest.UserId,
				changePasswordRequest.CurrentPassword,
				changePasswordRequest.NewPassword);
	}
}
