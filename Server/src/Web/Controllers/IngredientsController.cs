using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Commands.Create;
using CookingRecipesSystem.Application.Ingredients.Commands.Edit;
using CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient;
using CookingRecipesSystem.Application.Ingredients.Queries.GetIngredientsAll;
using CookingRecipesSystem.Web.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Controllers
{
	[Authorize]
	public class IngredientsController : BaseApiController
	{
		[HttpPost]
		[Route(nameof(Create))]
		public async Task<ActionResult<EntityKeyModel>> Create(
			[FromForm] CreateIngredientCommand command)
		{
			return await Send(command);
		}

		[HttpPost]
		[Route(nameof(Edit))]
		public async Task<ActionResult<IngredientResponseModel>> Edit(
			[FromForm] EditIngredientCommand command)
		{
			return await Send(command);
		}

		[AllowAnonymous]
		[HttpGet]
		[Route(nameof(GetById) + "/" + Id)]
		public async Task<ActionResult<IngredientResponseModel>> GetById(
			[FromRoute] GetIngredientByIdQuery query)
			=> await Send(query);

		[AllowAnonymous]
		[HttpGet]
		[Route(nameof(GetAll))]
		public async Task<ActionResult<IngredientsListResponseModel>> GetAll(
			[FromQuery] GetIngredientsAllQuery query)
			=> await Send(query);
	}
}
