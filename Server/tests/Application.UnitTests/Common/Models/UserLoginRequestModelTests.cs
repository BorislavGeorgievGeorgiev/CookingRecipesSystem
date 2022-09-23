using CookingRecipesSystem.Application.Identity.Commands.LoginUser;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class UserLoginRequestModelTests
	{
		private const string EmailPropertyName = "Email";
		private const string PasswordPropertyName = "Password";
		private const string EmailString = "test@test.com";
		private const string PasswordString = "strongPassword";

		private readonly Type _returnTypeString = typeof(string);
		private readonly UserLoginRequestModel _loginModel = new UserLoginRequestModel
		{
			Email = EmailString,
			Password = PasswordString
		};

		[Fact]
		public void Public_String_Email_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(_loginModel.PublicPropertyExist(EmailPropertyName, _returnTypeString));
		}

		[Fact]
		public void Email_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(_loginModel.Email == EmailString);
		}

		[Fact]
		public void Public_String_Password_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(_loginModel
				.PublicPropertyExist(PasswordPropertyName, _returnTypeString));
		}

		[Fact]
		public void Password_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(_loginModel.Password == PasswordString);
		}
	}
}
