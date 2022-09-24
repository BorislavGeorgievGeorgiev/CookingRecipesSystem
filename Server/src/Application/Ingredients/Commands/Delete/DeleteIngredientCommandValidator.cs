using FluentValidation;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Delete
{
	public class DeleteIngredientCommandValidator :
		AbstractValidator<DeleteIngredientCommand>
	{
		public DeleteIngredientCommandValidator()
		{
			RuleFor(i => i.Id)
					.NotEmpty();
		}
	}
}
