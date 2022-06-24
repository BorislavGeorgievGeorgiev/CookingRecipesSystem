
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class UserLoginRequestModelTests
	{
		private const string EmailString = "test@test.com";
		private const string PasswordString = "strongPassword";

		[Fact]
		public void Public_String_Email_Property_Should_Exist()
		{
			// Arrange			
			const string EmailPropertyName = "Email";
			var loginModel = new UserLoginRequestModel(EmailString, PasswordString);

			// Act & Assert
			Assert.True(loginModel.GetType().GetProperty(EmailPropertyName, typeof(string)) != null);
		}

		[Fact]
		public void Public_String_Password_Property_Should_Exist()
		{
			// Arrange			
			const string PasswordPropertyName = "Password";
			var loginModel = new UserLoginRequestModel(EmailString, PasswordString);

			// Act & Assert
			Assert.True(loginModel.GetType().GetProperty(PasswordPropertyName, typeof(string)) != null);
		}

		[Fact]
		public void Email_Should_Be_Set_Correctly()
		{
			// Arrange
			var loginModel = new UserLoginRequestModel(EmailString, PasswordString);

			// Act & Assert
			Assert.True(loginModel.Email == EmailString);
		}

		[Fact]
		public void Password_Should_Be_Set_Correctly()
		{
			// Arrange
			var loginModel = new UserLoginRequestModel(EmailString, PasswordString);

			// Act & Assert
			Assert.True(loginModel.Password == PasswordString);
		}
	}
}
