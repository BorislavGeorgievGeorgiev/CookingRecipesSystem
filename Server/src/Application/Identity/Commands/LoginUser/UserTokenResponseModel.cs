namespace CookingRecipesSystem.Application.Identity.Commands.LoginUser
{
	public class UserTokenResponseModel
	{
		public UserTokenResponseModel(string token)
			=> this.Token = token;

		public string Token { get; }
	}
}
