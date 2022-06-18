namespace CookingRecipesSystem.Application.Common.Models
{
	public class UserResponseModel
	{
		public UserResponseModel(string token)
			=> this.Token = token;

		public string Token { get; }
	}
}
