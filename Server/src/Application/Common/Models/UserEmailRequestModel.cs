namespace CookingRecipesSystem.Application.Common.Models
{
	public class UserEmailRequestModel
	{
		public UserEmailRequestModel(string email)
			=> this.Email = email;

		public string Email { get; }
	}
}
