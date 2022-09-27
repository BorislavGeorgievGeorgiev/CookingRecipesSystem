using FluentValidation;

namespace CookingRecipesSystem.Application.Identity.Commands.ChangePassword
{
	public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
	{
		public ChangePasswordCommandValidator()
		{
			RuleFor(u => u.UserId)
				.NotEmpty();

			RuleFor(u => u.CurrentPassword)
				.NotEmpty();

			RuleFor(u => u.NewPassword)
				.NotEmpty();
		}
	}
}
