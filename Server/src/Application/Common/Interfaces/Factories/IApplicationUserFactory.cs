namespace CookingRecipesSystem.Application.Common.Interfaces.Factories
{
	public interface IApplicationUserFactory : IFactory<IApplicationUser>
	{
		IApplicationUserFactory With(string userName, string email);
	}
}
