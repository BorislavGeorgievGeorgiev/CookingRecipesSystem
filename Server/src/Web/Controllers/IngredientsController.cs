using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Commands.Create;
using CookingRecipesSystem.Application.Ingredients.Commands.Delete;
using CookingRecipesSystem.Application.Ingredients.Commands.Update;
using CookingRecipesSystem.Application.Ingredients.Queries.GetAll;
using CookingRecipesSystem.Application.Ingredients.Queries.GetById;
using CookingRecipesSystem.Application.Ingredients.Queries.Search;
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
			[FromForm] IngredientCreateCommand command)
		{
			return await Send(command);
		}

		[HttpPut]
		[Route(nameof(Edit))]
		public async Task<ActionResult<IngredientResponseModel>> Edit(
			[FromForm] IngredientUpdateCommand command)
		{
			return await Send(command);
		}

		[HttpDelete]
		[Route(nameof(Delete))]
		public async Task<ActionResult> Delete(
			[FromQuery] IngredientDeleteCommand command)
		{
			return await Send(command);
		}

		[AllowAnonymous]
		[HttpGet]
		[Route(nameof(GetById) + "/" + Id)]
		public async Task<ActionResult<IngredientResponseModel>> GetById(
			[FromRoute] IngredientGetByIdQuery query)
			=> await Send(query);

		[AllowAnonymous]
		[HttpGet]
		[Route(nameof(GetAll))]
		public async Task<ActionResult<IEnumerable<IngredientResponseModel>>> GetAll(
			[FromQuery] IngredientGetAllQuery query)
			=> await Send(query);

		[AllowAnonymous]
		[HttpGet]
		[Route(nameof(Search))]
		public async Task<ActionResult<IEnumerable<IngredientResponseModel>>> Search(
			[FromQuery] IngredientSearchQuery query)
			=> await Send(query);
	}
}
