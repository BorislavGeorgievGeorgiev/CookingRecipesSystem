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
			_userManager = userManager;
		}

		public async Task<ApplicationResult> ChangePasswordAsync(
			IApplicationUser user, string currentPassword, string newPassword)
		{
			var identityResult = await _userManager.ChangePasswordAsync(
					(ApplicationUser)user, currentPassword, newPassword);

			return identityResult.ToApplicationResult();
		}

		public async Task<ApplicationResult<CheckPasswordModel>> CheckPasswordAsync(
			IApplicationUser user, string password)
		{
			var isValidPassword = await _userManager
				.CheckPasswordAsync((ApplicationUser)user, password);

			if (!isValidPassword)
			{
				return ApplicationResult<CheckPasswordModel>.Failure(ExceptionMessages.PasswordInvalid);
			}

			var response = new CheckPasswordModel(isValidPassword);

			return ApplicationResult<CheckPasswordModel>.Success(response);
		}

		public async Task<ApplicationResult> CreateAsync(IApplicationUser user, string password)
		{
			var identityResult = await _userManager.CreateAsync((ApplicationUser)user, password);

			return identityResult.ToApplicationResult();
		}

		public async Task<ApplicationResult<IApplicationUser>> FindByEmailAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			return UserResult(user);
		}

		public async Task<ApplicationResult<IApplicationUser>> FindByIdAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			return UserResult(user);
		}

		public async Task<ApplicationResult<IList<string>>> GetRolesAsync(IApplicationUser user)
		{
			var roles = await _userManager.GetRolesAsync((ApplicationUser)user);

			return ApplicationResult<IList<string>>.Success(roles);
		}

		public ApplicationResult<IQueryable<IApplicationUser>> GetAll()
		{
			var users = _userManager.Users;

			return ApplicationResult<IQueryable<IApplicationUser>>.Success(users);
		}

		public ApplicationResult<IQueryable<IApplicationUser>> GetAllAsNoTracking()
		{
			var users = _userManager.Users.AsNoTracking();

			return ApplicationResult<IQueryable<IApplicationUser>>.Success(users);
		}

		private ApplicationResult<IApplicationUser> UserResult(ApplicationUser? user)
		{
			if (user == null)
			{
				return ApplicationResult<IApplicationUser>.Failure(ExceptionMessages.UserInvalid);
			}

			return ApplicationResult<IApplicationUser>.Success(user);
		}
	}
}
