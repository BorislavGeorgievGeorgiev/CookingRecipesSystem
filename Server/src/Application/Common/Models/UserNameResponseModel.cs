namespace CookingRecipesSystem.Application.Common.Models
{
	public class UserNameResponseModel
	{
		public UserNameResponseModel(string userName)
			=> this.UserName = userName;

		public string UserName { get; }
	}
}
