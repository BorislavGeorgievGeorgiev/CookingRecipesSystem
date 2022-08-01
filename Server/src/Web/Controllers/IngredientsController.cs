using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Commands;
using CookingRecipesSystem.Application.Ingredients.Queries.GetIngredientsAll;
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
		public async Task<ActionResult<ApplicationResult>> Create(
			[FromForm] CreateIngredientCommand command)
		{
			return await this.Send(command);
		}

		[HttpGet]
		[Route(nameof(GetAll))]
		public async Task<ActionResult<ApplicationResult<IngredientsListResponseModel>>> GetAll(
			[FromQuery] GetIngredientsAllQuery query)
			=> await this.Send(query);
	}
}
