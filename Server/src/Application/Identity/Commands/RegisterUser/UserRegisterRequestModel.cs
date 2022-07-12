using CookingRecipesSystem.Application.Identity.Commands.LoginUser;

namespace CookingRecipesSystem.Application.Identity.Commands.RegisterUser
{
	public class UserRegisterRequestModel : UserLoginRequestModel
	{
		public UserRegisterRequestModel(string userName, string email, string password)
			: base(email, password)
		{
			this.UserName = userName;
		}

		public string UserName { get; }
	}
}
