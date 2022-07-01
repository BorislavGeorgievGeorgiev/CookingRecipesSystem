namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IApplicationUserFactory : IFactory<IApplicationUser>
	{
		IApplicationUserFactory WithUserNameAndEmail(string userName, string email);
	}
}
