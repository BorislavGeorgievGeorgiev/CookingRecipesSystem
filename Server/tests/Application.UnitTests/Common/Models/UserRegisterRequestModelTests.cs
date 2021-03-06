using CookingRecipesSystem.Application.Identity.Commands.RegisterUser;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class UserRegisterRequestModelTests
	{
		private const string UserNamePropertyName = "UserName";
		private const string UserNameString = "User";
		private const string EmailString = "test@test.com";
		private const string PasswordString = "strongPassword";

		private readonly Type _returnTypeString = typeof(string);
		private readonly UserRegisterRequestModel _registerModel = new UserRegisterRequestModel(
			UserNameString, EmailString, PasswordString);

		[Fact]
		public void Public_String_UserName_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._registerModel
				.PublicPropertyExist(UserNamePropertyName, this._returnTypeString));
		}

		[Fact]
		public void Public_String_UserName_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._registerModel
				.PublicPropertyCanWrite(UserNamePropertyName, this._returnTypeString));
		}

		[Fact]
		public void UserName_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._registerModel.UserName == UserNameString);
		}
	}
}
