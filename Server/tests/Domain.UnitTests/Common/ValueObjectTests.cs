using CookingRecipesSystem.Domain.Common;

using FluentAssertions;

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
			result.Should().BeTrue();
		}

		[Fact]
		public void ValueObjects_With_Equal_Properties_Should_Not_Be_Diferent()
		{
			// Arrange
			var first = new TestValueObject();
			var second = new TestValueObject();

			// Act
			var result = first != second;

			// Assert
			result.Should().BeFalse();
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
			result.Should().BeFalse();
		}

		[Fact]
		public void ValueObjects_With_Different_Properties_Should_Be_Diferent()
		{
			// Arrange
			var first = new TestValueObject();
			var second = new TestValueObject2();

			// Act
			var result = first != second;

			// Assert
			result.Should().BeTrue();
		}
	}
}
