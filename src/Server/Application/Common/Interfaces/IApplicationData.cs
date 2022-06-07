namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IApplicationData
	{
		Task<int> SaveAsync(CancellationToken cancellationToken);
	}
}
