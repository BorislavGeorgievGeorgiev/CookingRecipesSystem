namespace CookingRecipesSystem.Domain.Common
{
	public interface IDeletableEntity
	{
		bool IsDeleted { get; set; }

		DateTime? DeletedOn { get; set; }
	}
}
