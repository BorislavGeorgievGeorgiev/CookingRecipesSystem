using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Entities;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient
{
  public class IngredientResponseModel : EntityKeyResponseModel, IMapFrom<Ingredient>
  {
    public string Name { get; set; }

    public string Description { get; set; }

    public PhotoResponseModel Photo { get; set; }

    public ICollection<RecipeNavigationResponseModel> Recipes { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<Ingredient, IngredientResponseModel>()
          .ForMember(d => d.Photo, opt => opt.MapFrom(s => s.Photo))
          .ForMember(d => d.Recipes, opt => opt.MapFrom(s => s.Recipes));
    }
  }
}
