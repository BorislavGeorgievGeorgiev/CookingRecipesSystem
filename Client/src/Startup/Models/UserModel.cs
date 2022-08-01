using System.ComponentModel.DataAnnotations;

namespace CookingRecipesSystem.Startup.Models
{
	public class UserModel
	{
		[Required]
		public string UserName { get; set; }
	}
}
