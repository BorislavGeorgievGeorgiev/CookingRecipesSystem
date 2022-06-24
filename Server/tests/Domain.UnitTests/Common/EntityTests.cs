using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.UnitTests.Common
{
	public class EntityTests
	{
		private class TestEntity : Entity<int> { }
		private class TestEntity2 : IEntity<int>
		{
			public TestEntity2(int id) => this.Id = id;

			public int Id { get; }
		}
		private class TestEntity3 : Entity<int> { }

		[Fact]
		public void When_Other_Not_Inherit_EntityTKey_Should_Be_Not_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity2(1);

			// Act
			var resultEqual = first.Equals(second);

			// Assert
			Assert.False(resultEqual);
		}

		[Fact]
		public void Entities_With_Same_Instances_Should_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = first;

			// Act
			var resultEqual = first == second;
			var resultDifferent = first != second;

			// Assert
			Assert.True(resultEqual);
			Assert.False(resultDifferent);
		}

		[Fact]
		public void Entities_With_Equal_Ids_Should_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity().SetId(1);

			// Act
			var resultEqual = first == second;
			var resultDifferent = first != second;

			// Assert
			Assert.True(resultEqual);
			Assert.False(resultDifferent);
		}

		[Fact]
		public void Entities_With_Equal_HashCode_Should_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity().SetId(1);

			// Act
			var resultEqual = first.GetHashCode() == second.GetHashCode();
			var resultDifferent = first.GetHashCode() != second.GetHashCode();

			// Assert
			Assert.True(resultEqual);
			Assert.False(resultDifferent);
		}

		[Fact]
		public void Entities_With_Different_Type_Should_Not_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity3().SetId(1);

			// Act
			var resultEqual = first.Equals(second);

			// Assert
			Assert.False(resultEqual);
		}

		[Fact]
		public void Entities_With_Different_Ids_Should_Not_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity().SetId(2);

			// Act
			var resultEqual = first == second;
			var resultDifferent = first != second;

			// Assert
			Assert.False(resultEqual);
			Assert.True(resultDifferent);
		}

		[Fact]
		public void Entities_With_Different_HashCode_Should_Not_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity().SetId(2);

			// Act
			var resultEqual = first.GetHashCode() == second.GetHashCode();
			var resultDifferent = first.GetHashCode() != second.GetHashCode();

			// Assert
			Assert.False(resultEqual);
			Assert.True(resultDifferent);
		}

		[Fact]
		public void When_Other_Entity_Is_Null_Entities_Should_Not_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			TestEntity second = null!;

			// Act
			var resultEqual = first == second;
			var resultDifferent = first != second;

			// Assert
			Assert.False(resultEqual);
			Assert.True(resultDifferent);
		}

		[Fact]
		public void When_Entities_Are_Null_Should_Be_Equal()
		{
			// Arrange
			TestEntity first = null!;
			TestEntity second = null!;

			// Act
			var resultEqual = first == second;
			var resultDifferent = first != second;

			// Assert
			Assert.False(resultDifferent);
			Assert.True(resultEqual);
		}
	}
}
