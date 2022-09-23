using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Create
{
  public class CreateIngredientCommandValidator :
    AbstractValidator<CreateIngredientCommand>
  {
    public CreateIngredientCommandValidator()
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
