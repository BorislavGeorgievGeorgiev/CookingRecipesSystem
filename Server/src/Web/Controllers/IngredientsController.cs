﻿using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Commands;
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
		public async Task<ActionResult<ApplicationResult>> Create(
			[FromForm] CreateIngredientCommand command)
		{
			return await Send(command);
		}

		[AllowAnonymous]
		[HttpGet]
		[Route(nameof(GetIngredient) + Id)]
		public async Task<ActionResult<ApplicationResult<IngredientResponseModel>>> GetIngredient(
			[FromRoute] GetIngredientQuery query)
			=> await Send(query);

		[AllowAnonymous]
		[HttpGet]
		[Route(nameof(GetAll))]
		public async Task<ActionResult<ApplicationResult<IngredientsListResponseModel>>> GetAll(
			[FromQuery] GetIngredientsAllQuery query)
			=> await Send(query);
	}
}
