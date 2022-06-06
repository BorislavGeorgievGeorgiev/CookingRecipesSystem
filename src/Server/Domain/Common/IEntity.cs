namespace CookingRecipesSystem.Domain.Common
{
	public interface IEntity<TKey>
	{
		public TKey Id { get; set; }
	}
}
