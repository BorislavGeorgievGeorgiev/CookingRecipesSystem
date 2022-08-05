﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Blazored.LocalStorage;

using CookingRecipesSystem.Startup.Constants;

using Microsoft.AspNetCore.Components.Authorization;

namespace CookingRecipesSystem.Startup.Services
{
	public class ApiAuthenticationStateProvider : AuthenticationStateProvider
	{
		private const string AuthenticationType = "jwt";
		private readonly ILocalStorageService _localStorage;

		public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
		{
			this._localStorage = localStorage;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var savedToken = await this._localStorage.GetItemAsync<string>(AppConstants.AuthTokenName);

			if (string.IsNullOrWhiteSpace(savedToken))
			{
				return new AuthenticationState(GetAnonymousUser());
			}

			var claims = ParseClaimsFromJwt(savedToken);
			var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, AuthenticationType));
			var authenticationState = new AuthenticationState(claimsPrincipal);

			var authenticationStateTask = Task.FromResult(authenticationState);
			this.NotifyAuthenticationStateChanged(authenticationStateTask);

			return authenticationState;
		}

		public void MarkUserAsLoggedOut()
		{
			var anonymousUser = GetAnonymousUser();
			var authenticationStateTask = Task.FromResult(new AuthenticationState(anonymousUser));
			this.NotifyAuthenticationStateChanged(authenticationStateTask);
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

		private static ClaimsPrincipal GetAnonymousUser()
		{
			return new ClaimsPrincipal(new ClaimsIdentity());
		}
	}
}
