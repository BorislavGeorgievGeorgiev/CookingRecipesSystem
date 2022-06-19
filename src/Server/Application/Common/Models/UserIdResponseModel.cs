namespace CookingRecipesSystem.Application.Common.Models
{
	public class UserIdResponseModel
	{
		public UserIdResponseModel(string userId)
			=> this.UserId = userId;

		public string UserId { get; }
	}
}
