using CookingRecipesSystem.Application.Identity.Commands.LoginUser;

namespace CookingRecipesSystem.Application.Identity.Commands.RegisterUser
{
	public class UserRegisterRequestModel : UserLoginRequestModel
	{
		public string UserName { get; set; }
	}
}
