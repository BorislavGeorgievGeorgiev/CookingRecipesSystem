using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Identity.Commands.LoginUser
{
	public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
	{
		public LoginUserCommandValidator()
		{
			this.RuleFor(u => u.Email)
				.EmailAddress()
				.NotEmpty();

			this.RuleFor(u => u.Password)
				.MinimumLength(AppConstants.PasswordMinLength)
				.MaximumLength(AppConstants.PasswordMaxLength)
				.NotEmpty();
		}
	}
}
