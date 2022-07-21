using CookingRecipesSystem.Startup.Models;

using Microsoft.AspNetCore.Components;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IAuthenticationService
	{
		User User { get; }

		Task Initialize();

		Task Login(string username, string password);

		Task Logout();
	}

	public class AuthenticationService : IAuthenticationService
	{
		private IHttpService _httpService;
		private NavigationManager _navigationManager;
		private ILocalStorageService _localStorageService;

		public User User { get; private set; }

		public AuthenticationService(
				IHttpService httpService,
				NavigationManager navigationManager,
				ILocalStorageService localStorageService
		)
		{
			this._httpService = httpService;
			this._navigationManager = navigationManager;
			this._localStorageService = localStorageService;
		}

		public async Task Initialize()
		{
			this.User = await this._localStorageService.GetItem<User>("user");
		}

		public async Task Login(string username, string password)
		{
			this.User = await this._httpService.Post<User>("/users/authenticate", new { username, password });
			await this._localStorageService.SetItem("user", this.User);
		}

		public async Task Logout()
		{
			this.User = null;
			await this._localStorageService.RemoveItem("user");
			this._navigationManager.NavigateTo("login");
		}
	}
}
