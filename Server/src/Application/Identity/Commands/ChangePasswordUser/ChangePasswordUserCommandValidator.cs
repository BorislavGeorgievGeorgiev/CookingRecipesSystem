using FluentValidation;

namespace CookingRecipesSystem.Application.Identity.Commands.ChangePasswordUser
{
	public class ChangePasswordUserCommandValidator : AbstractValidator<ChangePasswordUserCommand>
	{
		public ChangePasswordUserCommandValidator()
		{
			this.RuleFor(u => u.UserId)
				.NotEmpty();

			this.RuleFor(u => u.CurrentPassword)
				.NotEmpty();

			this.RuleFor(u => u.NewPassword)
				.NotEmpty();
		}
	}
}
