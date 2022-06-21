namespace CookingRecipesSystem.Application.Common.Models
{
	public class CheckPasswordModel
	{
		public CheckPasswordModel(bool isValidPassword)
			=> this.IsValidPassword = isValidPassword;

		public bool IsValidPassword { get; } = false;
	}
}
