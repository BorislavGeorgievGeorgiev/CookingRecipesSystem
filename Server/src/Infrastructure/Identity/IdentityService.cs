using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Identity
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IJwtService _jwtService;

		public IdentityService(
			UserManager<ApplicationUser> userManager,
			IJwtService jwtService)
		{
			this._userManager = userManager;
			this._jwtService = jwtService;
		}

		public async Task<ApplicationResult<UserNameResponseModel>> GetUserName(
			UserIdRequestModel userIdRequest)
		{
			var userName = await this._userManager
				.Users
				.Where(u => u.Id == userIdRequest.UserId)
				.Select(u => u.UserName)
				.FirstOrDefaultAsync();

			if (userName == null)
			{
				return ApplicationResult<UserNameResponseModel>.Failure(ExceptionMessages.NoUser);
			}

			var response = new UserNameResponseModel(userName);

			return ApplicationResult<UserNameResponseModel>.Success(response);
		}

		public async Task<ApplicationResult<UserIdResponseModel>> FindUserIdByEmail(
			UserEmailRequestModel emailRequest)
		{
			var user = await this._userManager.FindByEmailAsync(emailRequest.Email);

			if (user == null)
			{
				return ApplicationResult<UserIdResponseModel>.Failure(ExceptionMessages.NoUser);
			}

			var response = new UserIdResponseModel(user.Id);

			return ApplicationResult<UserIdResponseModel>.Success(response);
		}

		public async Task<ApplicationResult> ChangePassword(
			ChangePasswordRequestModel changePasswordRequest)
		{
			var user = await this._userManager.FindByIdAsync(changePasswordRequest.UserId);

			if (user == null)
			{
				return ApplicationResult.Failure(ExceptionMessages.NoUser);
			}

			var identityResult = await this._userManager.ChangePasswordAsync(
					user, changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword);

			return identityResult.ToApplicationResult();
		}

		public async Task<ApplicationResult<UserIdResponseModel>> Register(
			UserRegisterRequestModel userRequest)
		{
			var user = new ApplicationUser
			{
				UserName = userRequest.UserName,
				Email = userRequest.Email,
			};

			var identityResult = await this._userManager.CreateAsync(user, userRequest.Password);

			var response = new UserIdResponseModel(user.Id);

			return identityResult.ToApplicationResult(response);
		}

		public async Task<ApplicationResult<UserTokenResponseModel>> Login(
			UserLoginRequestModel userRequest)
		{
			var user = await this._userManager.FindByEmailAsync(userRequest.Email);

			if (user == null)
			{
				return ApplicationResult<UserTokenResponseModel>.Failure(ExceptionMessages.InvalidCredentials);
			}

			var isValidPassword = await this._userManager.CheckPasswordAsync(user, userRequest.Password);

			if (!isValidPassword)
			{
				return ApplicationResult<UserTokenResponseModel>.Failure(ExceptionMessages.InvalidCredentials);
			}

			var token = await this._jwtService.GenerateToken(user.Id, user.Email);

			var response = new UserTokenResponseModel(token);

			return ApplicationResult<UserTokenResponseModel>.Success(response);
		}

		public async Task<ApplicationResult> DeleteUser(UserIdRequestModel userIdRequest)
		{
			var user = this._userManager
					.Users
					.SingleOrDefault(u => u.Id == userIdRequest.UserId);

			if (user == null)
			{
				return ApplicationResult.Failure(ExceptionMessages.NoUser);
			}

			return await this.DeleteUser(user);
		}

		public async Task<ApplicationResult> DeleteUser(ApplicationUser user)
		{
			var result = await this._userManager.DeleteAsync(user);

			return result.ToApplicationResult();
		}
	}
}
