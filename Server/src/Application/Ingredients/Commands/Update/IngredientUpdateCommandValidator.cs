using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Update
{
	public class IngredientUpdateCommandValidator :
		AbstractValidator<IngredientUpdateCommand>
	{
		public IngredientUpdateCommandValidator()
		{
			RuleFor(i => i.Name)
					.MaximumLength(EntityConstants.IngredientNameMaxLength)
					.NotEmpty();

			RuleFor(i => i.Description)
					.MaximumLength(EntityConstants.IngredientDescriptionMaxLength)
					.NotEmpty();
		}
	}
}
