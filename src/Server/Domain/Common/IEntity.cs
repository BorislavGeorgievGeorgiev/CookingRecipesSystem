namespace CookingRecipesSystem.Domain.Common
{
	public interface IEntity<TKey> where TKey : struct
	{
		public TKey Id { get; }
	}
}
