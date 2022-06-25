using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class UserIdResponseModelTests
	{
		private const string UserIdStringPropertyName = "UserId";
		private const string UserIdString = "1";

		private readonly Type _returnTypeString = typeof(string);
		private readonly UserIdResponseModel _userIdModel = new UserIdResponseModel(UserIdString);

		[Fact]
		public void Public_String_UserId_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._userIdModel
				.PublicPropertyExist(UserIdStringPropertyName, this._returnTypeString));
		}

		[Fact]
		public void Public_String_UserId_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._userIdModel
				.PublicPropertyCanWrite(UserIdStringPropertyName, this._returnTypeString));
		}

		[Fact]
		public void UserId_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._userIdModel.UserId == UserIdString);
		}
	}
}
