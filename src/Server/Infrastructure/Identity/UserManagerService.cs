using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Identity
{
	public class UserManagerService : IUserManagerService
	{
		private const string NoUser = "There is no such user.";
		private const string NoValidPassowrd = "The passowrd is not valid.";

		private readonly UserManager<ApplicationUser> _userManager;

		public UserManagerService(UserManager<ApplicationUser> userManager)
				=> this._userManager = userManager;

		public async Task<(ApplicationResult Result, string? UserName)> GetUserName(string userId)
		{
			var userName = await this._userManager
				.Users
				.Where(u => u.Id == userId)
				.Select(u => u.UserName)
				.FirstOrDefaultAsync();

			if (userName == null)
			{
				return (ApplicationResult.Failure(NoUser), null);
			}

			return (ApplicationResult.Success, userName);
		}

		public async Task<(ApplicationResult Result, string? UserId)> FindUserIdByEmail(string email)
		{
			var user = await this._userManager.FindByEmailAsync(email);

			if (user == null)
			{
				return (ApplicationResult.Failure(NoUser), null);
			}

			return (ApplicationResult.Success, user.Id);
		}

		public async Task<(ApplicationResult Result, bool IsRightPassowrd)> CheckPassword(string userId, string password)
		{
			var user = await this._userManager.FindByIdAsync(userId);

			var isValidPassword = await this._userManager.CheckPasswordAsync(user, password);

			if (!isValidPassword)
			{
				return (ApplicationResult.Failure(NoValidPassowrd), false);
			}

			return (ApplicationResult.Success, isValidPassword);
		}

		public async Task<ApplicationResult> ChangePassword(
			string userId, string currentPassword, string newPassowrd)
		{
			var user = await this._userManager.FindByIdAsync(userId);

			var identityResult = await this._userManager.ChangePasswordAsync(
					user, currentPassword, newPassowrd);

			return identityResult.ToApplicationResult();
		}

		public async Task<(ApplicationResult Result, string UserId)> CreateUser(
			string userName, string email, string password)
		{
			var user = new ApplicationUser
			{
				UserName = userName,
				Email = email,
			};

			var identityResult = await this._userManager.CreateAsync(user, password);

			return (identityResult.ToApplicationResult(), user.Id);
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
