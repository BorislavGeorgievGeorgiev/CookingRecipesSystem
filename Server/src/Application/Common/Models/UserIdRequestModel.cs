namespace CookingRecipesSystem.Application.Common.Models
{
	public class UserIdRequestModel
	{
		public UserIdRequestModel(string userId)
			=> this.UserId = userId;

		public string UserId { get; }
	}
}
