using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Identity.Commands.Login
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(u => u.Email)
				.NotEmpty()
				.Matches(AppConstants.EmailRegEx);

			RuleFor(u => u.Password)
				.NotEmpty();
		}
	}
}
