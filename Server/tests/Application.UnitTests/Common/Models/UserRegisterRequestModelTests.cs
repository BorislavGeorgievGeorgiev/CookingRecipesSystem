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
		private readonly UserRegisterRequestModel _registerModel = new UserRegisterRequestModel
		{
			UserName = UserNameString,
			Email = EmailString,
			Password = PasswordString
		};

		[Fact]
		public void Public_String_UserName_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(_registerModel
				.PublicPropertyExist(UserNamePropertyName, _returnTypeString));
		}

		[Fact]
		public void UserName_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(_registerModel.UserName == UserNameString);
		}
	}
}
