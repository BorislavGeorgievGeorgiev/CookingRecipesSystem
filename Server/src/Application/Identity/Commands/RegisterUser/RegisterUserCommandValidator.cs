using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Identity.Commands.RegisterUser
{
	public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
	{
		public RegisterUserCommandValidator()
		{
			this.RuleFor(u => u.UserName)
				.MinimumLength(AppConstants.UserNameMinLength)
				.MaximumLength(AppConstants.UserNameMaxLength)
				.NotEmpty();

			this.RuleFor(u => u.Email)
				.NotEmpty()
				.Matches(AppConstants.EmailRegEx);

			this.RuleFor(u => u.Password)
				.MinimumLength(AppConstants.PasswordMinLength)
				.MaximumLength(AppConstants.PasswordMaxLength)
				.NotEmpty();
		}
	}
}
