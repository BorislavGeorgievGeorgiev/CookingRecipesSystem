namespace CookingRecipesSystem.Application.Identity.Commands.Login
{
	public class TokenResponseModel
	{
		public TokenResponseModel(string token)
			=> Token = token;

		public string Token { get; }
	}
}
