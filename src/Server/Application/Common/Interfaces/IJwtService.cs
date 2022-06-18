namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IJwtService
	{
		Task<string> GenerateToken(string userId, string userEmail);
	}
}