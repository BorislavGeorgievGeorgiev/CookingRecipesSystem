
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class UserLoginRequestModelTests
	{
		private const string EmailPropertyName = "Email";
		private const string PasswordPropertyName = "Password";
		private const string EmailString = "test@test.com";
		private const string PasswordString = "strongPassword";

		private readonly UserLoginRequestModel _loginModel = new UserLoginRequestModel(
			EmailString, PasswordString);

		[Fact]
		public void Public_String_Email_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._loginModel.PublicPropertyExist(EmailPropertyName, typeof(string)));
		}

		[Fact]
		public void Public_String_Email_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._loginModel.PublicPropertyCanWrite(EmailPropertyName));
		}

		[Fact]
		public void Email_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._loginModel.Email == EmailString);
		}

		[Fact]
		public void Public_String_Password_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._loginModel.PublicPropertyExist(PasswordPropertyName, typeof(string)));
		}

		[Fact]
		public void Public_String_Password_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._loginModel.PublicPropertyCanWrite(PasswordPropertyName));
		}

		[Fact]
		public void Password_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._loginModel.Password == PasswordString);
		}
	}
}
