using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class UserTokenResponseModelTests
	{
		[Fact]
		public void Public_String_Token_Property_Should_Exist()
		{
			// Arrange
			const string TokenPropertyName = "Token";
			var tokenModel = new UserTokenResponseModel("someString");

			// Act & Assert
			Assert.True(tokenModel.GetType().GetProperty(TokenPropertyName, typeof(string)) != null);
		}

		[Fact]
		public void Token_Should_Be_Set_Correctly()
		{
			// Arrange
			var stringToken = "token";
			var tokenModel = new UserTokenResponseModel(stringToken);

			// Act & Assert
			Assert.True(tokenModel.Token == stringToken);
		}
	}
}
