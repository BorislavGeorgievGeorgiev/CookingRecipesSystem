using System.ComponentModel.DataAnnotations;

using CookingRecipesSystem.Startup.Constants;

namespace CookingRecipesSystem.Startup.Models
{
  public class UserModel
  {
    [Required]
    [Display(Name = AppConstants.UserNameDisplay)]
    public string? UserName { get; set; }
  }
}
