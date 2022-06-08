
using CookingRecipesSystem.Application.Common.Models;

using Microsoft.AspNetCore.Identity;

namespace CookingRecipesSystem.Infrastructure.Identity
{
	public static class IdentityResultExtensions
	{
		public static ApplicationResult ToApplicationResult(this IdentityResult result)
				=> result.Succeeded
						? ApplicationResult.Success
						: ApplicationResult.Failure(result.Errors.Select(e => e.Description));
	}
}
