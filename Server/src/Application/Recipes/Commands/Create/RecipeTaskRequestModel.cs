using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.Recipes.Commands.Create
{
	public class RecipeTaskRequestModel : IMapTo<RecipeTask>
	{
		public byte Position { get; set; }

		public string Description { get; set; }

		public IFormFile Photo { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<RecipeTaskRequestModel, RecipeTask>()
							.ForMember(d => d.Photo, opt => opt.Ignore());
		}
	}
}
