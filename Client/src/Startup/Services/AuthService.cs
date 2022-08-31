using System.Net.Http.Json;

using Blazored.LocalStorage;

using CookingRecipesSystem.Startup.Constants;
using CookingRecipesSystem.Startup.Extensions;
using CookingRecipesSystem.Startup.Helpers;
using CookingRecipesSystem.Startup.Models;
using CookingRecipesSystem.Startup.Models.User;

namespace CookingRecipesSystem.Startup.Services
{
    public interface IAuthService
  {
    Task<AppResult<TokenResponseModel>> Login(UserLoginModel loginModel);
    Task Logout();
    Task<AppResult> Register(UserRegisterModel registerModel);

    Task<AppResult<UsersListModel>> GetAll();
  }

  public class AuthService : IAuthService
  {
    private const string IdentityControllerName = "Identity";

    private readonly HttpClient _httpClient;
    private readonly ConfigurationHelper _configurationHelper;
    private readonly ApiAuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(
      HttpClient httpClient,
      ConfigurationHelper configurationHelper,
      ApiAuthenticationStateProvider authenticationStateProvider,
      ILocalStorageService localStorage)
    {
      _httpClient = httpClient;
      _configurationHelper = configurationHelper;
      _authenticationStateProvider = authenticationStateProvider;
      _localStorage = localStorage;
    }

    public async Task<AppResult> Register(UserRegisterModel registerModel)
    {
      HttpResponseMessage? response = null;
      AppResult result;

      try
      {
        var uri = _configurationHelper
          .GetRequestUri(IdentityControllerName, nameof(Register));
        response = await _httpClient.PostAsJsonAsync(uri, registerModel);
      }
      catch (Exception ex)
      {
        //TODO: Log exception.
        return AppResult.Failure(ErrorMessages.ServerError);
      }

      result = await response.DeserializeResponseAsync();

      return result!;
    }

    public async Task<AppResult<TokenResponseModel>> Login(UserLoginModel loginModel)
    {
      HttpResponseMessage? response = null;
      AppResult<TokenResponseModel> result;

      try
      {
        var uri = _configurationHelper
          .GetRequestUri(IdentityControllerName, nameof(Login));
        response = await _httpClient.PostAsJsonAsync(uri, loginModel);
      }
      catch (Exception ex)
      {
        //TODO: Log exception.
        return AppResult<TokenResponseModel>.Failure(ErrorMessages.ServerError);
      }

      result = await response.DeserializeResponseAsync<TokenResponseModel>();

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
        return AppResult<TokenResponseModel>.Failure("User is not authenticated.");
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
      var uri = _configurationHelper
        .GetRequestUri(IdentityControllerName, nameof(GetAll));

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
  }
}
