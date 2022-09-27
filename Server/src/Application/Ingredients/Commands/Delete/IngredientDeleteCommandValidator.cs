using FluentValidation;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Delete
{
	public class IngredientDeleteCommandValidator :
		AbstractValidator<IngredientDeleteCommand>
	{
		public IngredientDeleteCommandValidator()
		{
			RuleFor(i => i.Id)
					.NotEmpty();
		}
	}
}
