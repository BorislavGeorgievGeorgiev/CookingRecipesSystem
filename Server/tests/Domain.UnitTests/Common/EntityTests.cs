using CookingRecipesSystem.Domain.Common;

using FluentAssertions;

namespace CookingRecipesSystem.Domain.UnitTests.Common
{
	public class EntityTests
	{
		[Fact]
		public void Entities_With_Equal_Ids_Should_Be_Equal()
		{
			// Arrange
			var first = new TestEntity().SetId(1);
			var second = new TestEntity().SetId(1);

			// Act
			var result = first == second;

			// Assert
			result.Should().BeTrue();
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
			result.Should().BeFalse();
		}

		private class TestEntity : Entity<int> { }
	}

	internal static class EntityExtensions
	{
		public static TEntity SetId<TEntity>(this TEntity entity, int id)
				where TEntity : Entity<int>
				=> (entity.SetId<int>(id) as TEntity)!;

		private static Entity<T> SetId<T>(this Entity<T> entity, int id)
				where T : struct
		{
			entity
					.GetType()
					.BaseType!
					.GetProperty(nameof(Entity<T>.Id))!
					.GetSetMethod(true)!
					.Invoke(entity, new object[] { id });

			return entity;
		}
	}
}
