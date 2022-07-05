using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Infrastructure.Identity;

namespace CookingRecipesSystem.Infrastructure.Common
{
	public class ApplicationUserFactory : IApplicationUserFactory
	{
		private readonly ApplicationUser _newUser;

		public ApplicationUserFactory()
		{
			this._newUser = Activator.CreateInstance<ApplicationUser>();
		}

		public IApplicationUser Create()
		{
			return this._newUser;
		}

		public IApplicationUserFactory WithUserNameAndEmail(string userName, string email)
		{
			Guard.IsNotNullOrWhiteSpace(userName, nameof(ApplicationUser.UserName));
			Guard.IsNotNullOrWhiteSpace(email, nameof(ApplicationUser.Email));

			this._newUser.UserName = userName;
			this._newUser.Email = email;

			return this;
		}
	}
}
