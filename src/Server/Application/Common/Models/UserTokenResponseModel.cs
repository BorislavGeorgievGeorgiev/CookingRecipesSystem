namespace CookingRecipesSystem.Application.Common.Models
{
	public class UserTokenResponseModel
	{
		public UserTokenResponseModel(string token)
			=> this.Token = token;

		public string Token { get; }
	}
}
