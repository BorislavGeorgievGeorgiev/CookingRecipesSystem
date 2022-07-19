﻿using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.Ingredients.Commands
{
	public class IngredientRequestModel : IMapTo<Ingredient>
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public IFormFile PhotoFile { get; set; }
	}
}
