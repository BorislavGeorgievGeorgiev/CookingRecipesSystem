using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Identity
{
	public class IdentityService : IIdentityService
	{
		private const string InvalidCredentials = "Invalid credentials.";
		private const string InvalidPassowrd = "Invalid passowrd.";

		private readonly IUserManagerService _userManagerService;
		private readonly IJwtService _jwtService;

		public IdentityService(
			IUserManagerService userManagerService,
			IJwtService jwtService)
		{
			this._userManagerService = userManagerService;
			this._jwtService = jwtService;
		}

		public async Task<(ApplicationResult Result, UserIdResponseModel Response)> Register(
			UserRequestModel userRequest)
		{
			var newUserResult = await this._userManagerService.CreateUser(
				userRequest.UserName, userRequest.Email, userRequest.Password);

			var response = new UserIdResponseModel(newUserResult.UserId);

			return (newUserResult.Result, response);
		}

		public async Task<(ApplicationResult Result, UserTokenResponseModel Response)> Login(
			UserRequestModel userRequest)
		{
			UserTokenResponseModel response;
			var userIdTuple = await this._userManagerService
				.FindUserIdByEmail(userRequest.Email);

			if (userIdTuple.UserId == null)
			{
				response = new UserTokenResponseModel(string.Empty);

				return (ApplicationResult.Failure(InvalidCredentials), response);
			}

			var isValidPasswordTuple = await this._userManagerService.CheckPassword(
				userIdTuple.UserId!, userRequest.Password);

			if (!isValidPasswordTuple.IsRightPassowrd)
			{
				response = new UserTokenResponseModel(string.Empty);

				return (ApplicationResult.Failure(InvalidPassowrd), response);
			}

			var token = await this._jwtService.GenerateToken(
				userIdTuple.UserId!, userRequest.Email);

			response = new UserTokenResponseModel(token);

			return (ApplicationResult.Success, response);
		}

		public Task<ApplicationResult> ChangePassword(
			ChangePasswordRequestModel changePasswordRequest)
			=> this._userManagerService.ChangePassword(
				changePasswordRequest.UserId,
				changePasswordRequest.CurrentPassword,
				changePasswordRequest.NewPassword);
	}
}
