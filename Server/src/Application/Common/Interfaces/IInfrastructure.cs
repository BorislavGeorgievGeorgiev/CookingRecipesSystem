namespace CookingRecipesSystem.Application.Common.Interfaces
{
	/// <summary>
	/// Interface implemented by DbSet<TEntity>.Namespace Microsoft.EntityFrameworkCore.Infrastructure.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IInfrastructure<out T>
	{
		T Instance { get; }
	}
}
