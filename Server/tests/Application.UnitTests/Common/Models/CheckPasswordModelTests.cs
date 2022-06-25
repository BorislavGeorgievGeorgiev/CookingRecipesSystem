using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class CheckPasswordModelTests
	{
		private const string IsValidPasswordPropertyName = "IsValidPassword";
		private const bool ValidPassword = true;

		private readonly CheckPasswordModel _checkPasswordModelValid = new CheckPasswordModel(
			ValidPassword);

		[Fact]
		public void Public_Bool_IsValidPassword_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._checkPasswordModelValid
				.PublicPropertyExist(IsValidPasswordPropertyName, typeof(bool)));
		}

		[Fact]
		public void Public_Bool_IsValidPassword_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._checkPasswordModelValid
				.PublicPropertyCanWrite(IsValidPasswordPropertyName));
		}

		[Fact]
		public void IsValidPassword_Should_Be_Set_Correctly()
		{
			// Arrange
			var checkPasswordModelNotValid = new CheckPasswordModel(false);

			// Act & Assert
			Assert.True(this._checkPasswordModelValid.IsValidPassword);
			Assert.False(checkPasswordModelNotValid.IsValidPassword);
		}
	}
}
