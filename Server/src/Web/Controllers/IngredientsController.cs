using CookingRecipesSystem.Application.Ingredients.Commands;
using CookingRecipesSystem.Web.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Controllers
{
	public class IngredientsController : BaseApiController
	{
		[Authorize]
		[HttpPost]
		[Route(nameof(Create))]
		public async Task<ActionResult> Create(
			[FromForm] CreateIngredientCommand command)
		{
			return await this.Send(command);
		}
	}
}
