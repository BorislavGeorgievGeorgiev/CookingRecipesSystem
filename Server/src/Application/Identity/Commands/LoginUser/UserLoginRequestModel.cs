namespace CookingRecipesSystem.Application.Identity.Commands.LoginUser
{
	public class UserLoginRequestModel
	{
		public UserLoginRequestModel(string email, string password)
		{
			this.Email = email;
			this.Password = password;
		}

		public string Email { get; }

		public string Password { get; }
	}
}
