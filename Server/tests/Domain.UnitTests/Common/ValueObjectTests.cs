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

			private bool Test2 { get; } = false;
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
	}
}
