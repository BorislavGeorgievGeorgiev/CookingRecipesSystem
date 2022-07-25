using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IUserManagerService : ITransientService
	{
		Task<ApplicationResult> CreateAsync(IApplicationUser user, string password);

		Task<ApplicationResult<IApplicationUser>> FindByEmailAsync(string email);

		Task<ApplicationResult<IApplicationUser>> FindByIdAsync(string userId);

		Task<ApplicationResult> ChangePasswordAsync(
			IApplicationUser user, string currentPassword, string newPassword);

		Task<ApplicationResult<CheckPasswordModel>> CheckPasswordAsync(
			IApplicationUser user, string password);

		ApplicationResult<IQueryable<IApplicationUser>> GetAll();

		ApplicationResult<IQueryable<IApplicationUser>> GetAllAsNoTracking();
	}
}
