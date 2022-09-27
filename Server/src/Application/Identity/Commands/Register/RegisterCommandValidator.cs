using CookingRecipesSystem.Domain.Common.Constants;

using FluentValidation;

namespace CookingRecipesSystem.Application.Identity.Commands.Register
{
	public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
	{
		public RegisterCommandValidator()
		{
			RuleFor(u => u.UserName)
				.MinimumLength(AppConstants.UserNameMinLength)
				.MaximumLength(AppConstants.UserNameMaxLength)
				.NotEmpty();

			RuleFor(u => u.Email)
				.NotEmpty()
				.Matches(AppConstants.EmailRegEx);

			RuleFor(u => u.Password)
				.MinimumLength(AppConstants.PasswordMinLength)
				.MaximumLength(AppConstants.PasswordMaxLength)
				.NotEmpty();
		}
	}
}
