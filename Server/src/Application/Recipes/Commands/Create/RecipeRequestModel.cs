﻿using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.Recipes.Commands.Create
{
	public class RecipeRequestModel : IMapTo<Recipe>
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public IFormFile Photo { get; set; }

		public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();

		public ICollection<RecipeTaskRequestModel> RecipeTasks { get; set; } = new HashSet<RecipeTaskRequestModel>();

		public void Mapping(Profile profile)
		{
			profile.CreateMap<RecipeRequestModel, Recipe>()
							.ForMember(d => d.Photo, opt => opt.Ignore());
			profile.CreateMap<RecipeRequestModel, Recipe>()
							.ForMember(d => d.RecipeTasks, opt => opt.Ignore());
		}
	}
}
