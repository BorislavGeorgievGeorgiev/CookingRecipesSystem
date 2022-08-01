using CookingRecipesSystem.Application.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Common.Extensions
{
	public static class ApplicationResultExtensions
	{
		public static async Task<ActionResult<ApplicationResult>> ToActionResult(
			this Task<ApplicationResult> resultTask)
		{
			var result = await resultTask;

			if (!result.Succeeded)
			{
				return new BadRequestObjectResult(result);
			}

			return new OkObjectResult(result);
		}

		public static async Task<ActionResult<ApplicationResult<TData>>> ToActionResult<TData>(
			this Task<ApplicationResult<TData>> resultTask)
			where TData : class
		{
			var result = await resultTask;

			if (!result.Succeeded)
			{
				return new BadRequestObjectResult(result);
			}

			return new OkObjectResult(result);
		}
	}
}
