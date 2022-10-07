using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Recipes.Commands.Create;
using CookingRecipesSystem.Web.Common;

using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Controllers
{
	public class RecipesController : BaseApiController
	{
		[HttpPost]
		[Route(nameof(Create))]
		public async Task<ActionResult<EntityKeyModel>> Create(
			[FromForm] RecipeCreateCommand command)
		{
			return await Send(command);
		}

		/*[HttpPut]
		[Route(nameof(Edit))]
		public async Task<ActionResult<RecipeResponseModel>> Edit(
			[FromForm] RecipeUpdateCommand command)
		{
			return await Send(command);
		}

		[HttpDelete]
		[Route(nameof(Delete))]
		public async Task<ActionResult> Delete(
			[FromQuery] RecipeDeleteCommand command)
		{
			return await Send(command);
		}*/
	}
}
