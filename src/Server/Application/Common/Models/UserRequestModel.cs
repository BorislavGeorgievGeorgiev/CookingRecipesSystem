namespace CookingRecipesSystem.Application.Common.Models
{
	public class UserRequestModel
	{
		protected UserRequestModel(string userName, string email, string password)
		{
			this.UserName = userName;
			this.Email = email;
			this.Password = password;
		}

		public string UserName { get; }

		public string Email { get; }

		public string Password { get; }
	}
}
