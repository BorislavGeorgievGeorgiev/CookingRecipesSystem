using System.Collections;
using System.ComponentModel;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	/// <summary>
	/// Interface for DbSet<TEntity>.Namespace Microsoft.EntityFrameworkCore.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IDbSet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IAsyncEnumerable<TEntity>, IInfrastructure<IServiceProvider>, IListSource
		where TEntity : class
	{
	}
}
