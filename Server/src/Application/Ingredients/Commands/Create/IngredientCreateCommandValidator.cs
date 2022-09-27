using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Create
{
  public class IngredientCreateCommandValidator :
    AbstractValidator<IngredientCreateCommand>
  {
    public IngredientCreateCommandValidator()
    {
      RuleFor(i => i.Name)
          .MaximumLength(EntityConstants.IngredientNameMaxLength)
          .NotEmpty();

      RuleFor(i => i.Description)
          .MaximumLength(EntityConstants.IngredientDescriptionMaxLength)
          .NotEmpty();

      RuleFor(i => i.Photo)
          .NotEmpty();
    }
  }
}
