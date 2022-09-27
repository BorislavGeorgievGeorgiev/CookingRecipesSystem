using CookingRecipesSystem.Application.Identity.Commands.Login;

namespace CookingRecipesSystem.Application.Identity.Commands.Register
{
	public class RegisterRequestModel : LoginRequestModel
	{
		public string UserName { get; set; }
	}
}
