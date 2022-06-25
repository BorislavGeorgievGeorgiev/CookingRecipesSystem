using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class UserNameResponseModelTests
	{
		private const string UserNameStringPropertyName = "UserName";
		private const string UserNameString = "User";

		private readonly UserNameResponseModel _userNameModel = new UserNameResponseModel(
			UserNameString);

		[Fact]
		public void Public_String_UserName_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._userNameModel
				.PublicPropertyExist(UserNameStringPropertyName, typeof(string)));
		}

		[Fact]
		public void Public_String_UserName_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._userNameModel.PublicPropertyCanWrite(UserNameStringPropertyName));
		}

		[Fact]
		public void UserName_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._userNameModel.UserName == UserNameString);
		}
	}
}
