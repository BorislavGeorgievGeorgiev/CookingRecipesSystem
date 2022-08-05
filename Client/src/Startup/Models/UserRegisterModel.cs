using System.ComponentModel.DataAnnotations;

using CookingRecipesSystem.Startup.Constants;

namespace CookingRecipesSystem.Startup.Models
{
	public class UserRegisterModel
	{
		[Required]
		[StringLength(AppConstants.UserNameMaxLength, ErrorMessage = ErrorMessages.InvalidStringLength,
			MinimumLength = AppConstants.UserNameMinLength)]
		[Display(Name = AppConstants.UserNameDisplay)]
		public string? UserName { get; set; }

		[Required]
		[RegularExpression(AppConstants.EmailRegEx, ErrorMessage = ErrorMessages.InvalidEmail)]
		public string? Email { get; set; }

		[Required]
		[StringLength(AppConstants.PasswordMaxLength, ErrorMessage = ErrorMessages.InvalidStringLength,
			MinimumLength = AppConstants.PasswordMinLength)]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = AppConstants.PasswordConfirmDisplay)]
		[Compare(nameof(Password), ErrorMessage = ErrorMessages.InvalidPasswordConfirm)]
		public string? PasswordConfirm { get; set; }
	}
}
