
using AutoFixture;

using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.UnitTests.Common.Models
{
	public class ApplicationResultTests
	{
		private const string SucceededPropertyName = "Succeeded";
		private const string ErrorsPropertyName = "Errors";
		private const string SuccessPropertyName = "Success";
		private const string FailurePropertyName = "Failure";
		private const bool SucceededPropertyData = true;

		private readonly Type _returnTypeBool;
		private readonly Type _returnTypeString;
		private readonly Type _returnTypeIEnumerableString;
		private readonly Type _returnTypeApplicationResult;
		private readonly ApplicationResult _applicationResult;
		private readonly IEnumerable<string> _errors;
		private readonly IFixture _fixture;

		public ApplicationResultTests()
		{
			this._fixture = new Fixture();
			this._returnTypeBool = typeof(bool);
			this._returnTypeString = typeof(string);
			this._returnTypeIEnumerableString = typeof(IEnumerable<string>);
			this._returnTypeApplicationResult = typeof(ApplicationResult);

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

		[Fact]
		public void Public_Static_ApplicationResult_Success_Property_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._applicationResult
				.PublicPropertyExist(SuccessPropertyName, this._returnTypeApplicationResult));

			Assert.True(this._applicationResult
				.PublicPropertyIsStatic(SuccessPropertyName, this._returnTypeApplicationResult));
		}

		[Fact]
		public void Public_Static_ApplicationResult_Success_Property_Should_Has_Private_Set()
		{
			// Arrange, Act & Assert
			Assert.False(this._applicationResult
				.PublicPropertyCanWrite(SuccessPropertyName, this._returnTypeApplicationResult));

		}

		[Fact]
		public void Success_Should_Return_Correctly()
		{
			// Arrange
			var appResSuccess = ApplicationResult.Success;

			// Act & Assert
			Assert.True(appResSuccess.Succeeded == true
				&& appResSuccess.Errors.SequenceEqual(Array.Empty<string>()));
		}

		[Fact]
		public void Public_Static_ApplicationResult_Failure_Method_Should_Exist()
		{
			// Arrange, Act & Assert
			Assert.True(this._applicationResult
				.PublicMethodExist(FailurePropertyName,
				this._returnTypeApplicationResult, new Type[] { this._returnTypeString }));

			Assert.True(this._applicationResult
				.PublicMethodExist(FailurePropertyName,
				this._returnTypeApplicationResult, new Type[] { this._returnTypeIEnumerableString }));

			Assert.True(this._applicationResult
				.PublicMethodIsStatic(FailurePropertyName, new Type[] { this._returnTypeString }));

			Assert.True(this._applicationResult
				.PublicMethodIsStatic(FailurePropertyName, new Type[] { this._returnTypeIEnumerableString }));
		}

		[Fact]
		public void Failure_Should_Return_Correctly()
		{
			const string SingleError = "error";

			// Arrange
			var appResFailureSingleError = ApplicationResult.Failure(SingleError);
			var appResFailureManyErrors = ApplicationResult.Failure(this._errors);

			// Act & Assert
			Assert.True(appResFailureSingleError.Succeeded == false
				&& appResFailureSingleError.Errors.SequenceEqual(new string[] { SingleError }));

			Assert.True(appResFailureManyErrors.Succeeded == false
				&& appResFailureManyErrors.Errors.SequenceEqual(this._errors));
		}
	}
}
