using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Infrastructure.Common.Extensions;
using CookingRecipesSystem.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Services
{
	public class UserManagerService : IUserManagerService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserManagerService(UserManager<ApplicationUser> userManager)
		{
			this._userManager = userManager;
		}

		public async Task<ApplicationResult> ChangePasswordAsync(
			IApplicationUser user, string currentPassword, string newPassword)
		{
			var identityResult = await this._userManager.ChangePasswordAsync(
					(ApplicationUser)user, currentPassword, newPassword);

			return identityResult.ToApplicationResult();
		}

		public async Task<ApplicationResult<CheckPasswordModel>> CheckPasswordAsync(
			IApplicationUser user, string password)
		{
			var isValidPassword = await this._userManager
				.CheckPasswordAsync((ApplicationUser)user, password);

			if (!isValidPassword)
			{
				return ApplicationResult<CheckPasswordModel>.Failure(ExceptionMessages.InvalidCredentials);
			}

			var response = new CheckPasswordModel(isValidPassword);

			return ApplicationResult<CheckPasswordModel>.Success(response);
		}

		public async Task<ApplicationResult> CreateAsync(IApplicationUser user, string password)
		{
			var identityResult = await this._userManager.CreateAsync((ApplicationUser)user, password);

			return identityResult.ToApplicationResult();
		}

		public async Task<ApplicationResult<IApplicationUser>> FindByEmailAsync(string email)
		{
			var user = await this._userManager.FindByEmailAsync(email);

			return this.UserResult(user);
		}

		public async Task<ApplicationResult<IApplicationUser>> FindByIdAsync(string userId)
		{
			var user = await this._userManager.FindByIdAsync(userId);

			return this.UserResult(user);
		}

		public ApplicationResult<IQueryable<IApplicationUser>> GetAll()
		{
			var users = this._userManager.Users;

			return ApplicationResult<IQueryable<IApplicationUser>>.Success(users);
		}

		public ApplicationResult<IQueryable<IApplicationUser>> GetAllNoTracking()
		{
			var users = this._userManager.Users.AsNoTracking();

			return ApplicationResult<IQueryable<IApplicationUser>>.Success(users);
		}

		private ApplicationResult<IApplicationUser> UserResult(ApplicationUser? user)
		{
			if (user == null)
			{
				return ApplicationResult<IApplicationUser>.Failure(ExceptionMessages.NoUser);
			}

			return ApplicationResult<IApplicationUser>.Success(user);
		}
	}
}
