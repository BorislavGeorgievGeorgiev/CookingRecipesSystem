using CookingRecipesSystem.Web.Common;

using Microsoft.AspNetCore.Authorization;

namespace CookingRecipesSystem.Web.Controllers
{
	[Authorize]
	public class RecipesController : BaseApiController
	{
		/*[HttpPost]
		[Route(nameof(Create))]
		public async Task<ActionResult<EntityKeyModel>> Create(
			[FromForm] RecipeCreateCommand command)
		{
			return await Send(command);
		}*/

		/*[HttpPost]
		[Route(nameof(Edit))]
		public async Task<ActionResult<RecipeResponseModel>> Edit(
			[FromForm] RecipeUpdateCommand command)
		{
			return await Send(command);
		}

		[HttpPost]
		[Route(nameof(Delete))]
		public async Task<ActionResult> Delete(
			[FromQuery] RecipeDeleteCommand command)
		{
			return await Send(command);
		}*/
	}
}
