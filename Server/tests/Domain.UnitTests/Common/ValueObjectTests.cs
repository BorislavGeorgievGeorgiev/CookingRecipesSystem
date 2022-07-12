using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.UnitTests.Common
{
	public class ValueObjectTests
	{
		private class TestValueObject : ValueObject
		{
			public int Test { get; } = default;
		}

		private class TestValueObject2 : ValueObject
		{
			public string Test { get; } = default!;
		}

		[Fact]
		public void ValueObjects_With_Equal_Properties_Should_Be_Equal()
		{
			// Arrange
			var first = new TestValueObject();
			var second = new TestValueObject();

			// Act
			var result = first == second;

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void ValueObjects_With_Equal_Properties_Should_Not_Be_Different()
		{
			// Arrange
			var first = new TestValueObject();
			var second = new TestValueObject();

			// Act
			var result = first != second;

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void ValueObjects_With_Different_Properties_Should_Not_Be_Equal()
		{
			// Arrange
			var first = new TestValueObject();
			var second = new TestValueObject2();

			// Act
			var result = first == second;

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void ValueObjects_With_Different_Properties_Should_Be_Different()
		{
			// Arrange
			var first = new TestValueObject();
			var second = new TestValueObject2();

			// Act
			var result = first != second;

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void ValueObjects_With_Different_HashCode_Should_Not_Be_Equal()
		{
			// Arrange
			var first = new TestValueObject();
			var second = new TestValueObject2();

			// Act
			var resultEqual = first.GetHashCode() == second.GetHashCode();
			var resultDifferent = first.GetHashCode() != second.GetHashCode();

			// Assert
			Assert.False(resultEqual);
			Assert.True(resultDifferent);
		}

		[Fact]
		public void ValueObjects_Should_Not_Be_Equal_When_One_Is_Null()
		{
			// Arrange
			var first = new TestValueObject();
			TestValueObject? second = null;

			// Act
			var resultEqual = first == second!;
			var resultEqual2 = second! == first;

			// Assert
			Assert.False(resultEqual);
			Assert.False(resultEqual2);
		}
	}
}
