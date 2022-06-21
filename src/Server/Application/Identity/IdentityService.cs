using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Identity
{
	public class IdentityService : IIdentityService
	{
		private const string InvalidCredentials = "Invalid credentials.";

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
		{
			var newUserResult = await this._userManagerService.CreateUser(
				userRequest.UserName, userRequest.Email, userRequest.Password);

			var response = new UserIdResponseModel(newUserResult.UserId);

			return ApplicationResult<UserIdResponseModel>.Success(response);
		}

		public async Task<ApplicationResult<UserTokenResponseModel>> Login(
			UserLoginRequestModel userRequest)
		{
			var resultUserId = await this._userManagerService
				.FindUserIdByEmail(userRequest.Email);

			var userId = resultUserId.Response;

			if (userId == null)
			{
				return ApplicationResult<UserTokenResponseModel>.Failure(InvalidCredentials);
			}

			var resultCheckPassword = await this._userManagerService.CheckPassword(
				userId, userRequest.Password);

			var isValidPassword = resultCheckPassword.Response.IsValidPassword;

			if (!isValidPassword)
			{
				return ApplicationResult<UserTokenResponseModel>.Failure(InvalidCredentials);
			}

			var token = await this._jwtService.GenerateToken(userId, userRequest.Email);

			var response = new UserTokenResponseModel(token);

			return ApplicationResult<UserTokenResponseModel>.Success(response);
		}

		public Task<ApplicationResult> ChangePassword(
			ChangePasswordRequestModel changePasswordRequest)
			=> this._userManagerService.ChangePassword(
				changePasswordRequest.UserId,
				changePasswordRequest.CurrentPassword,
				changePasswordRequest.NewPassword);
	}
}
