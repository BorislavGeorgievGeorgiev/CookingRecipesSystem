
using CookingRecipesSystem.Domain.Common;

using FluentValidation;

namespace CookingRecipesSystem.Application.Identity.Commands.RegisterUser
{
	public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
	{
		public RegisterUserCommandValidator()
		{
			this.RuleFor(u => u.UserName)
				.MinimumLength(ApplicationConstants.UserNameMinLength)
				.MaximumLength(ApplicationConstants.UserNameMaxLength)
				.NotEmpty();

			this.RuleFor(u => u.Email)
				.EmailAddress()
				.NotEmpty();

			this.RuleFor(u => u.Password)
				.MinimumLength(ApplicationConstants.PasswordMinLength)
				.MaximumLength(ApplicationConstants.PasswordMaxLength)
				.NotEmpty();
		}
	}
}
