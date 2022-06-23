using CookingRecipesSystem.Domain.Common;

namespace CookingRecipesSystem.Domain.UnitTests.Common
{
	internal static class EntityExtensions
	{
		/// <summary>
		/// Extend the current class to use Entity&lt;int&gt; extension method.
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		/// <param name="id"></param>
		/// <returns>Extended current class.</returns>
		internal static TEntity SetId<TEntity>(this TEntity entity, int id)
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
