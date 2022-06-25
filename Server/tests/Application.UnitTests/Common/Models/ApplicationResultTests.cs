using AutoFixture;

using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class ApplicationResultTests
	{
		private const string SucceededPropertyName = "Succeeded";
		private const string ErrorsPropertyName = "Errors";
		private const bool SucceededPropertyData = true;

		private readonly Type _returnTypeBool;
		private readonly Type _returnTypeIEnumerableString;
		private readonly ApplicationResult _applicationResult;
		private readonly IEnumerable<string> _errors;

		private readonly IFixture _fixture;

		public ApplicationResultTests()
		{
			this._fixture = new Fixture();
			this._returnTypeBool = typeof(bool);
			this._returnTypeIEnumerableString = typeof(IEnumerable<string>);

			this._errors = this._fixture.CreateMany<string>();

			this._applicationResult = new ApplicationResult(SucceededPropertyData, this._errors);
		}

		[Fact]
		public void Public_Bool_Succeeded_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._applicationResult
				.PublicPropertyExist(SucceededPropertyName, this._returnTypeBool));
		}

		[Fact]
		public void Public_Bool_Succeeded_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._applicationResult
				.PublicPropertyCanWrite(SucceededPropertyName, this._returnTypeBool));

		}

		[Fact]
		public void Succeeded_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._applicationResult.Succeeded == SucceededPropertyData);
		}

		[Fact]
		public void Public_IEnumerableString_Errors_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._applicationResult
				.PublicPropertyExist(ErrorsPropertyName, this._returnTypeIEnumerableString));
		}

		[Fact]
		public void Public_IEnumerableString_Errors_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._applicationResult
				.PublicPropertyCanWrite(ErrorsPropertyName, this._returnTypeIEnumerableString));

		}

		[Fact]
		public void Errors_Should_Be_Set_Correctly()
		{
			// Arrange, Act & Assert
			Assert.True(this._applicationResult.Errors.SequenceEqual(this._errors));
		}
	}
}
