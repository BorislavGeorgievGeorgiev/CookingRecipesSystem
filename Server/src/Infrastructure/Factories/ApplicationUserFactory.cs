using CookingRecipesSystem.Application.Common.Exceptions;
using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Interfaces.Factories;
using CookingRecipesSystem.Infrastructure.Identity;

namespace CookingRecipesSystem.Infrastructure.Factories
{
	public class ApplicationUserFactory : IApplicationUserFactory
	{
		private bool _isSet = false;

		private readonly ApplicationUser _newUser;

		public ApplicationUserFactory()
		{
			this._newUser = Activator.CreateInstance<ApplicationUser>();
		}

		public IApplicationUser Create()
		{
			if (this._isSet == false)
			{
				throw new NotCreatedException(nameof(ApplicationUser));
			}

			return this._newUser;
		}

		public IApplicationUserFactory With(string userName, string email)
		{
			this._newUser.UserName = userName;
			this._newUser.Email = email;
			this._isSet = true;

			return this;
		}
	}
}
