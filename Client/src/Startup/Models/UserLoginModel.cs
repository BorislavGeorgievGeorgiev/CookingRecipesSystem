using System.ComponentModel.DataAnnotations;

using CookingRecipesSystem.Startup.Shared;

namespace CookingRecipesSystem.Startup.Models
{
	public class UserLoginModel
	{
		[Required]
		[RegularExpression(AppConstants.EmailRegEx, ErrorMessage = ErrorMessages.InvalidEmail)]
		public string? Email { get; set; }

		[Required]
		[StringLength(AppConstants.PasswordMaxLength, ErrorMessage = ErrorMessages.InvalidStringLength,
			MinimumLength = AppConstants.PasswordMinLength)]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}
