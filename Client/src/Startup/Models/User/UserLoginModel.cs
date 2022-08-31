using System.ComponentModel.DataAnnotations;

using CookingRecipesSystem.Startup.Constants;

namespace CookingRecipesSystem.Startup.Models.User
{
  public class UserLoginModel
  {
    [Required]
    [RegularExpression(AppConstants.EmailRegEx, ErrorMessage = ErrorMessages.InvalidEmail)]
    [Display(Name = AppConstants.EmailDisplay)]
    public string? Email { get; set; }

    [Required]
    [StringLength(AppConstants.PasswordMaxLength, ErrorMessage = ErrorMessages.InvalidStringLength,
      MinimumLength = AppConstants.PasswordMinLength)]
    [DataType(DataType.Password)]
    [Display(Name = AppConstants.PasswordDisplay)]
    public string? Password { get; set; }
  }
}
