namespace CookingRecipesSystem.Application.Common.Models
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
