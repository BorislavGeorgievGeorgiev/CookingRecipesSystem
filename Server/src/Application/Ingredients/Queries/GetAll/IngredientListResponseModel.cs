using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetAll
{
	public class IngredientListResponseModel : IMapFrom<IEnumerable<IngredientResponseModel>>
	{
		public IEnumerable<IngredientResponseModel> Ingredients { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<IEnumerable<IngredientResponseModel>, IngredientListResponseModel>()
				.ForMember(d => d.Ingredients, opt => opt.MapFrom(s => s));
		}
	}
}
