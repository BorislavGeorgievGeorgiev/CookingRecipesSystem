
using CommunityToolkit.Diagnostics;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Infrastructure.Identity;

namespace CookingRecipesSystem.Infrastructure.Common
{
	public class ApplicationUserFactory : IApplicationUserFactory
	{
		private string? _userName;
		private string? _email;

		public IApplicationUser Create()
		{
			Guard.IsNotNullOrWhiteSpace(this._userName, nameof(ApplicationUser.UserName));
			Guard.IsNotNullOrWhiteSpace(this._email, nameof(ApplicationUser.Email));

			return new ApplicationUser
			{
				UserName = this._userName,
				Email = this._email,
			};
		}

		public IApplicationUserFactory WithUserNameAndEmail(string userName, string email)
		{
			this._userName = userName;
			this._email = email;

			return this;
		}
	}
}
