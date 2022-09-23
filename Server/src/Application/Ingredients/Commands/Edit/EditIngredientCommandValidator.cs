using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Edit
{
	public class EditIngredientCommandValidator :
		AbstractValidator<EditIngredientCommand>
	{
		public EditIngredientCommandValidator()
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
