using CookingRecipesSystem.Application.Identity.Commands.ChangePasswordUser;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class ChangePasswordRequestModelTests
	{
		private const string UserIdStringPropertyName = "UserId";
		private const string CurrentPasswordStringPropertyName = "CurrentPassword";
		private const string NewPasswordStringPropertyName = "NewPassword";
		private const string UserIdString = "1";
		private const string CurrentPasswordString = "CurrentPassword12";
		private const string NewPasswordString = "NewPassword12";

		private readonly Type _returnTypeString = typeof(string);
		private readonly ChangePasswordRequestModel _changePasswordRequestModel =
			new ChangePasswordRequestModel(UserIdString, CurrentPasswordString, NewPasswordString);

		[Fact]
		public void Public_String_UserId_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._changePasswordRequestModel
				.PublicPropertyExist(UserIdStringPropertyName, this._returnTypeString));
		}

		[Fact]
		public void Public_String_UserId_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._changePasswordRequestModel
				.PublicPropertyCanWrite(UserIdStringPropertyName, this._returnTypeString));
		}

		[Fact]
		public void UserId_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._changePasswordRequestModel.UserId == UserIdString);
		}

		[Fact]
		public void Public_String_CurrentPassword_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._changePasswordRequestModel
				.PublicPropertyExist(CurrentPasswordStringPropertyName, this._returnTypeString));
		}

		[Fact]
		public void Public_String_CurrentPassword_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._changePasswordRequestModel
				.PublicPropertyCanWrite(CurrentPasswordStringPropertyName, this._returnTypeString));
		}

		[Fact]
		public void CurrentPassword_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._changePasswordRequestModel.CurrentPassword == CurrentPasswordString);
		}

		[Fact]
		public void Public_String_NewPassword_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._changePasswordRequestModel
				.PublicPropertyExist(NewPasswordStringPropertyName, this._returnTypeString));
		}

		[Fact]
		public void Public_String_NewPassword_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._changePasswordRequestModel
				.PublicPropertyCanWrite(NewPasswordStringPropertyName, this._returnTypeString));
		}

		[Fact]
		public void NewPassword_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._changePasswordRequestModel.NewPassword == NewPasswordString);
		}
	}
}
