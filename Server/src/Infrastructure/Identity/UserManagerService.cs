using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Identity
{
	public class UserManagerService : IUserManagerService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserManagerService(UserManager<ApplicationUser> userManager)
				=> this._userManager = userManager;

		public async Task<ApplicationResult<UserNameResponseModel>> GetUserName(string userId)
		{
			var userName = await this._userManager
				.Users
				.Where(u => u.Id == userId)
				.Select(u => u.UserName)
				.FirstOrDefaultAsync();

			if (userName == null)
			{
				return ApplicationResult<UserNameResponseModel>.Failure(ExceptionMessages.NoUser);
			}

			var response = new UserNameResponseModel(userName);

			return ApplicationResult<UserNameResponseModel>.Success(response);
		}

		public async Task<ApplicationResult<UserIdResponseModel>> FindUserIdByEmail(string email)
		{
			var user = await this._userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return ApplicationResult<UserIdResponseModel>.Failure(ExceptionMessages.NoUser);
			}

			var response = new UserIdResponseModel(user.Id);

			return ApplicationResult<UserIdResponseModel>.Success(response);
		}

		public async Task<ApplicationResult<CheckPasswordModel>> CheckPassword(string userId, string password)
		{
			var user = await this._userManager.FindByIdAsync(userId);

			var isValidPassword = await this._userManager.CheckPasswordAsync(user, password);

			if (!isValidPassword)
			{
				return ApplicationResult<CheckPasswordModel>.Failure(ExceptionMessages.InvalidCredentials);
			}

			var response = new CheckPasswordModel(isValidPassword);

			return ApplicationResult<CheckPasswordModel>.Success(response);
		}

		public async Task<ApplicationResult> ChangePassword(
			string userId, string currentPassword, string newPassowrd)
		{
			var user = await this._userManager.FindByIdAsync(userId);

			var identityResult = await this._userManager.ChangePasswordAsync(
					user, currentPassword, newPassowrd);

			return identityResult.ToApplicationResult();
		}

		public async Task<ApplicationResult<UserIdResponseModel>> CreateUser(
			string userName, string email, string password)
		{
			var user = new ApplicationUser
			{
				UserName = userName,
				Email = email,
			};

			var identityResult = await this._userManager.CreateAsync(user, password);

			var response = new UserIdResponseModel(user.Id);

			return identityResult.ToApplicationResult(response);
		}

		public async Task<ApplicationResult> DeleteUser(string userId)
		{
			var user = this._userManager
					.Users
					.SingleOrDefault(u => u.Id == userId);

			if (user != null)
			{
				return await this.DeleteUser(user);
			}

			return ApplicationResult.Success;
		}

		public async Task<ApplicationResult> DeleteUser(ApplicationUser user)
		{
			var result = await this._userManager.DeleteAsync(user);

			return result.ToApplicationResult();
		}

	}
}
