using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Ingredients.Commands
{
	public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
	{
		public CreateIngredientCommandValidator()
		{
			this.RuleFor(i => i.Name)
				.MaximumLength(EntityConstants.IngredientNameMaxLength)
				.NotEmpty();

			this.RuleFor(i => i.Description)
				.MaximumLength(EntityConstants.IngredientDescriptionMaxLength)
				.NotEmpty();

			this.RuleFor(i => i.Photo)
				.NotEmpty();
		}
	}
}
