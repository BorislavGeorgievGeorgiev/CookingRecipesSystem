using System.ComponentModel.DataAnnotations;

namespace CookingRecipesSystem.Startup.Models
{
	public class UserLoginModel
	{
		[Required]
		public string? Email { get; set; }

		[Required]
		public string? Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
