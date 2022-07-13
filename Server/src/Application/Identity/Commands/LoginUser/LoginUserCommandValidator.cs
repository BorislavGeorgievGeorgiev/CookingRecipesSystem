
using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Identity.Commands.LoginUser
{
	public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
	{
		public LoginUserCommandValidator()
		{
			this.RuleFor(u => u.Email)
				.NotEmpty()
				.Matches(AppConstants.EmailRegEx);

			this.RuleFor(u => u.Password)
				.NotEmpty();
		}
	}
}
