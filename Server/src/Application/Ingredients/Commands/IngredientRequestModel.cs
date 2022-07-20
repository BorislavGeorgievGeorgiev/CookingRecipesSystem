using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.Ingredients.Commands
{
	public class IngredientRequestModel : IMapTo<Ingredient>
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public IFormFile Photo { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<IngredientRequestModel, Ingredient>()
					.ForMember(d => d.Photo, opt => opt.Ignore());
		}
	}
}
