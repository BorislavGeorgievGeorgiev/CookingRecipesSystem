using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

using Blazored.LocalStorage;

using CookingRecipesSystem.Startup.Constants;

using Microsoft.AspNetCore.Components.Authorization;

namespace CookingRecipesSystem.Startup.Services
{
  public class ApiAuthenticationStateProvider : AuthenticationStateProvider
  {
    private const string AuthenticationType = "jwt";
    private const string ClaimTypeExpiration = "exp";

    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    public ApiAuthenticationStateProvider(
      ILocalStorageService localStorage,
      HttpClient httpClient)
    {
      _localStorage = localStorage;
      _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var savedToken = await _localStorage.GetItemAsync<string>(AppConstants.AuthTokenName);

      if (string.IsNullOrWhiteSpace(savedToken))
      {
        return new AuthenticationState(GetEmptyShellUser());
      }

      var claims = ParseClaimsFromJwt(savedToken);
      var expiration = claims.First(c => c.Type == ClaimTypeExpiration).Value;
      var expirationDate = DateTimeOffset
        .FromUnixTimeSeconds(long.Parse(expiration)).DateTime;
      var isExpired = expirationDate < DateTime.UtcNow;

      if (isExpired)
      {
        MarkUserAsLoggedOut();
        return new AuthenticationState(GetEmptyShellUser());
      }

      var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, AuthenticationType));
      var authenticationState = new AuthenticationState(claimsPrincipal);

      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        AppConstants.BearerName, savedToken);

      return authenticationState;
    }

    public void MarkUserAsAuthenticated(AuthenticationState authenticationState)
    {
      var authenticationStateTask = Task.FromResult(authenticationState);
      NotifyAuthenticationStateChanged(authenticationStateTask);
    }

    public void MarkUserAsLoggedOut()
    {
      var authenticationStateTask = Task.FromResult(
        new AuthenticationState(GetEmptyShellUser()));
      NotifyAuthenticationStateChanged(authenticationStateTask);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
      var handler = new JwtSecurityTokenHandler();

      var token = handler.ReadJwtToken(jwt);
      var claims = token.Claims;

      var newClaims = new List<Claim>();

      foreach (var claim in claims)
      {
        switch (claim.Type)
        {
          case nameof(ClaimTypes.Name):
            newClaims.Add(new Claim(ClaimTypes.Name, claim.Value));
            break;
          case nameof(ClaimTypes.NameIdentifier):
            newClaims.Add(new Claim(ClaimTypes.NameIdentifier, claim.Value));
            break;
          case nameof(ClaimTypes.Email):
            newClaims.Add(new Claim(ClaimTypes.Email, claim.Value));
            break;
          case nameof(ClaimTypes.Role):
            newClaims.Add(new Claim(ClaimTypes.Role, claim.Value));
            break;
          default:
            newClaims.Add(new Claim(claim.Type, claim.Value));
            break;
        }
      }

      return newClaims;
    }

    private static ClaimsPrincipal GetEmptyShellUser()
    {
      return new ClaimsPrincipal(new ClaimsIdentity());
    }
  }
}
