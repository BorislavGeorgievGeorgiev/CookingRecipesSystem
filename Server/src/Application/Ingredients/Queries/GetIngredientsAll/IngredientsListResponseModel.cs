using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetIngredientsAll
{
	public class IngredientsListResponseModel : IMapFrom<IEnumerable<IngredientResponseModel>>
	{
		public IEnumerable<IngredientResponseModel> Ingredients { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<IEnumerable<IngredientResponseModel>, IngredientsListResponseModel>()
				.ForMember(d => d.Ingredients, opt => opt.MapFrom(s => s));
		}
	}
}
