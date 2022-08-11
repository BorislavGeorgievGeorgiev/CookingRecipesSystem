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
    private const string IdentityPath = "/Identity/";

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
      _httpClient = httpClient;
      _configuration = configuration;
      _apiIdentityUri = _configuration
        .GetSection(nameof(ApiConfig))
        .GetSection(nameof(ApiConfig.ApiUrl)).Value + IdentityPath;
      _authenticationStateProvider = authenticationStateProvider;
      _localStorage = localStorage;
    }

    public async Task<AppResult> Register(UserRegisterModel registerModel)
    {
      HttpResponseMessage? response = null;
      AppResult result;

      try
      {
        response = await _httpClient
        .PostAsJsonAsync(GetRequestUri(nameof(Register)), registerModel);
      }
      catch (Exception ex)
      {
        //TODO: Log exception.
        return AppResult.Failure(ErrorMessages.ServerError);
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
        response = await _httpClient
        .PostAsJsonAsync(GetRequestUri(nameof(Login)), loginModel);
      }
      catch (Exception ex)
      {
        //TODO: Log exception.
        return AppResult<LoginResult>.Failure(ErrorMessages.ServerError);
      }

      result = await DeserializeResponseAsync<LoginResult>(response);

      if (response.IsSuccessStatusCode == false)
      {
        await Logout();
        return result;
      }

      await _localStorage.SetItemAsync(
        AppConstants.AuthTokenName, result.Response.Token);

      var authenticationState = await _authenticationStateProvider
        .GetAuthenticationStateAsync();

      if (authenticationState.User.Identity.IsAuthenticated == false)
      {
        await Logout();
        return AppResult<LoginResult>.Failure("User is not authenticated.");
      }

      _authenticationStateProvider.MarkUserAsAuthenticated(authenticationState);

      return result;
    }

    public async Task Logout()
    {
      await _localStorage.RemoveItemAsync(AppConstants.AuthTokenName);

      _httpClient.DefaultRequestHeaders.Authorization = null;
      _authenticationStateProvider.MarkUserAsLoggedOut();
    }

    public async Task<AppResult<UsersListModel>> GetAll()
    {
      var uri = GetRequestUri(nameof(GetAll));
      AppResult<UsersListModel>? result;

      try
      {
        result = await _httpClient.GetFromJsonAsync<AppResult<UsersListModel>>(uri);
      }
      catch (Exception ex)
      {
        //TODO: Log exception.
        result = AppResult<UsersListModel>.Failure(ErrorMessages.ServerError);
      }

      return result;
    }

    private string GetRequestUri(string action)
    {
      string uri = _apiIdentityUri + action;
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
