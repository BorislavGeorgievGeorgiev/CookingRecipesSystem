using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using Blazored.LocalStorage;

using CookingRecipesSystem.Startup.Models;
using CookingRecipesSystem.Startup.Shared;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IAuthService
	{
		Task<AppResult<LoginResult>> Login(UserLoginModel loginModel);
		Task Logout();
		Task<AppResult> Register(UserRegisterModel registerModel);

		Task<AppResult<UsersListModel>> GetAll();
	}

	public class AuthService : IAuthService
	{
		private const string IdentityPath = "api/Identity/";

		private readonly string? _apiIdentityUri;
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;
		private readonly ApiAuthenticationStateProvider _authenticationStateProvider;
		private readonly ILocalStorageService _localStorage;

		public AuthService(
			HttpClient httpClient,
			IConfiguration configuration,
			ApiAuthenticationStateProvider authenticationStateProvider,
			ILocalStorageService localStorage)
		{
			this._httpClient = httpClient;
			this._configuration = configuration;
			this._apiIdentityUri = this._configuration
				.GetSection(nameof(ApiConfig))
				.GetSection(nameof(ApiConfig.ApiUrl)).Value + IdentityPath;
			this._authenticationStateProvider = authenticationStateProvider;
			this._localStorage = localStorage;
		}

		public async Task<AppResult> Register(UserRegisterModel registerModel)
		{
			var response = await this._httpClient
				.PostAsJsonAsync(this.GetRequestUri(nameof(Register)), registerModel);

			var registerResult = JsonSerializer
				.Deserialize<AppResult>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return registerResult!;
		}

		public async Task<AppResult<LoginResult>> Login(UserLoginModel loginModel)
		{
			var response = await this._httpClient
				.PostAsJsonAsync(this.GetRequestUri(nameof(Login)), loginModel);

			var loginResult = JsonSerializer
				.Deserialize<AppResult<LoginResult>>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (response.IsSuccessStatusCode == false)
			{
				await this.Logout();
				return loginResult;
			}

			await this._localStorage.SetItemAsync(
				AppConstants.AuthTokenName, loginResult.Response.Token);

			var authenticationState = await this._authenticationStateProvider
				.GetAuthenticationStateAsync();

			if (authenticationState.User.Identity.IsAuthenticated == false)
			{
				await this.Logout();
				return AppResult<LoginResult>.Failure("User is not authenticated.");
			}

			this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				AppConstants.BearerName, loginResult.Response.Token);

			return loginResult;
		}

		public async Task Logout()
		{
			await this._localStorage.RemoveItemAsync(AppConstants.AuthTokenName);

			this._httpClient.DefaultRequestHeaders.Authorization = null;
			this._authenticationStateProvider.MarkUserAsLoggedOut();
		}

		public async Task<AppResult<UsersListModel>> GetAll()
		{
			var uri = this.GetRequestUri(nameof(GetAll));
			AppResult<UsersListModel>? response;

			try
			{
				response = await this._httpClient.GetFromJsonAsync<AppResult<UsersListModel>>(uri);
			}
			catch (Exception ex)
			{
				response = AppResult<UsersListModel>.Failure(ex.Message);
			}

			return response!;
		}

		private string GetRequestUri(string action)
		{
			string uri = this._apiIdentityUri + action;
			return uri.ToLower();
		}
	}
}
