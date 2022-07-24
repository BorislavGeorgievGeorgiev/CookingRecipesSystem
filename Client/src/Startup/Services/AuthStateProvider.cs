using System.Security.Claims;

using CookingRecipesSystem.Startup.Models;

using Microsoft.AspNetCore.Components.Authorization;

namespace CookingRecipesSystem.Startup.Services
{
	public class AuthStateProvider : AuthenticationStateProvider
	{
		private readonly IIdentityService _identity;
		private readonly CancellationToken _cancellationToken;
		private UserCurrentModel? _currentUser;

		public AuthStateProvider(
			IIdentityService identity, CancellationToken cancellationToken = default)
		{
			this._identity = identity;
			this._cancellationToken = cancellationToken;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var identity = new ClaimsIdentity();

			try
			{
				var userInfo = await this.GetCurrentUser();

				if (userInfo.IsAuthenticated)
				{
					var claims = new[] { new Claim(ClaimTypes.Name, this._currentUser!.UserName!) }
					.Concat(this._currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));

					identity = new ClaimsIdentity(claims, "Server authentication");
				}
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine("Request failed:" + ex.ToString());
			}
			return new AuthenticationState(new ClaimsPrincipal(identity));
		}

		private async Task<UserCurrentModel> GetCurrentUser()
		{
			if (this._currentUser != null && this._currentUser.IsAuthenticated)
			{
				return this._currentUser;
			}

			this._currentUser = await this._identity.CurrentUserInfo(this._cancellationToken);

			return this._currentUser;
		}

		public async Task Logout()
		{
			await this._identity.Logout(this._cancellationToken);

			this._currentUser = null;

			this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
		}

		public async Task Login(UserLoginModel model)
		{
			await this._identity.Login(model, this._cancellationToken);

			this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
		}

		public async Task Register(UserRegisterModel model)
		{
			await this._identity.Register(model, this._cancellationToken);

			this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
		}
	}
}
