using CookingRecipesSystem.Application.Identity.Commands.LoginUser;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class UserTokenResponseModelTests
	{
		private const string TokenPropertyName = "Token";
		private const string TokenString = "SomeTokenString";

		private readonly Type _returnTypeString = typeof(string);
		private readonly UserTokenResponseModel _tokenModel = new UserTokenResponseModel(TokenString);

		[Fact]
		public void Public_String_Token_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._tokenModel
				.PublicPropertyExist(TokenPropertyName, this._returnTypeString));

		}

		[Fact]
		public void Public_String_Token_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._tokenModel
				.PublicPropertyCanWrite(TokenPropertyName, this._returnTypeString));
		}

		[Fact]
		public void Token_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._tokenModel.Token == TokenString);
		}
	}
}
