using System.Net.Http.Json;
using System.Text.Json;

using Blazored.LocalStorage;

using CookingRecipesSystem.Startup.Constants;
using CookingRecipesSystem.Startup.Models;

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
			HttpResponseMessage? response = null;
			AppResult result;

			try
			{
				response = await this._httpClient
				.PostAsJsonAsync(this.GetRequestUri(nameof(Register)), registerModel);
			}
			catch (Exception ex)
			{
				return AppResult.Failure(ex.Message);
			}

			result = await DeserializeResponseAsync(response);

			return result!;
		}

		public async Task<AppResult<LoginResult>> Login(UserLoginModel loginModel)
		{
			HttpResponseMessage? response = null;
			AppResult<LoginResult> result;

			try
			{
				response = await this._httpClient
				.PostAsJsonAsync(this.GetRequestUri(nameof(Login)), loginModel);
			}
			catch (Exception ex)
			{
				return AppResult<LoginResult>.Failure(ex.Message);
			}

			result = await DeserializeResponseAsync<LoginResult>(response);

			if (response.IsSuccessStatusCode == false)
			{
				await this.Logout();
				return result;
			}

			await this._localStorage.SetItemAsync(
				AppConstants.AuthTokenName, result.Response.Token);

			var authenticationState = await this._authenticationStateProvider
				.GetAuthenticationStateAsync();

			if (authenticationState.User.Identity.IsAuthenticated == false)
			{
				await this.Logout();
				return AppResult<LoginResult>.Failure("User is not authenticated.");
			}

			this._authenticationStateProvider.MarkUserAsAuthenticated(authenticationState);

			return result;
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
			AppResult<UsersListModel>? result;

			try
			{
				result = await this._httpClient.GetFromJsonAsync<AppResult<UsersListModel>>(uri);
			}
			catch (Exception ex)
			{
				result = AppResult<UsersListModel>.Failure(ex.Message);
			}

			return result;
		}

		private string GetRequestUri(string action)
		{
			string uri = this._apiIdentityUri + action;
			return uri.ToLower();
		}

		private static async Task<AppResult> DeserializeResponseAsync(
			HttpResponseMessage? response)
		{
			var result = JsonSerializer
				.Deserialize<AppResult>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}

		private static async Task<AppResult<TModel>> DeserializeResponseAsync<TModel>(
			HttpResponseMessage? response)
			where TModel : class
		{
			var result = JsonSerializer
				.Deserialize<AppResult<TModel>>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
	}
}
