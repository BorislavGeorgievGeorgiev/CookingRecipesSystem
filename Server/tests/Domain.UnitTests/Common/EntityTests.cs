using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.UnitTests.Common
{
	public class EntityTests
	{
		private class TestEntity : Entity<int> { }

		[Fact]
		public void Entities_With_Equal_Ids_Should_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity().SetId(1);

			// Act
			var result = first == second;

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Entities_With_Different_Ids_Should_Not_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity().SetId(2);

			// Act
			var result = first == second;

			// Assert
			Assert.False(result);
		}
	}
}
