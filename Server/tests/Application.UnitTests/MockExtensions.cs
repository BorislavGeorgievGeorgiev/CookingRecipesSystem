using Moq;

namespace CookingRecipesSystem.Application.UnitTests
{
	public static class MockExtensions
	{
		public static void SetupIQueryable<TRepository, TEntity>(
			this Mock<TRepository> mock, IQueryable<TEntity> queryable)
			where TRepository : class, IQueryable<TEntity>
			where TEntity : class
		{
			mock.Setup(r => r.GetEnumerator()).Returns(() =>
			{
				var enumerator = queryable.GetEnumerator();
				enumerator.Reset();
				return enumerator;
			});
			mock.Setup(r => r.Provider).Returns(queryable.Provider);
			mock.Setup(r => r.ElementType).Returns(queryable.ElementType);
			mock.Setup(r => r.Expression).Returns(queryable.Expression);
		}
	}
}
