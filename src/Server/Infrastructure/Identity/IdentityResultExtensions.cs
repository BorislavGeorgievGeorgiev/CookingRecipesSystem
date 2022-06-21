
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

		public static ApplicationResult<Tresponse> ToApplicationResult<Tresponse>(this IdentityResult result, Tresponse response)
			where Tresponse : class
				=> result.Succeeded
						? ApplicationResult<Tresponse>.Success(response)
						: ApplicationResult<Tresponse>.Failure(result.Errors.Select(e => e.Description));
	}
}
