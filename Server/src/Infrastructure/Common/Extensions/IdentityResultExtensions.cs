using CookingRecipesSystem.Application.Common.Models;

using Microsoft.AspNetCore.Identity;

namespace CookingRecipesSystem.Infrastructure.Common.Extensions
{
	public static class IdentityResultExtensions
	{
		public static ApplicationResult ToApplicationResult(this IdentityResult result)
				=> result.Succeeded
						? ApplicationResult.Success
						: ApplicationResult.Failure(result.Errors.Select(e => e.Description));

		public static ApplicationResult<TResponse> ToApplicationResult<TResponse>(
			this IdentityResult result, TResponse response)
			where TResponse : class
				=> result.Succeeded
						? ApplicationResult<TResponse>.Success(response)
						: ApplicationResult<TResponse>.Failure(result.Errors.Select(e => e.Description));
	}
}
