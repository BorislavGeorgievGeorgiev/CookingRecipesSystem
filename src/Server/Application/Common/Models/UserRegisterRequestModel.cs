namespace CookingRecipesSystem.Application.Common.Models
{
	public class UserRegisterRequestModel : UserLoginRequestModel
	{
		protected UserRegisterRequestModel(string userName, string email, string password)
			: base(email, password)
		{
			this.UserName = userName;
		}

		public string UserName { get; }
	}
}
