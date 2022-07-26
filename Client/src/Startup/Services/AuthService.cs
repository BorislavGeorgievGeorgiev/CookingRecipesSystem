﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using Blazored.LocalStorage;

using CookingRecipesSystem.Startup.Models;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IAuthService
	{
		Task<LoginResult> Login(UserLoginModel loginModel);
		Task Logout();
		Task<RegisterResult> Register(UserRegisterModel registerModel);

		Task<UsersListModel> GetAll();
	}

	public class AuthService : IAuthService
	{
		private readonly string? _apiIdentity;
		private readonly HttpClient _httpClient;
		private readonly ApiAuthenticationStateProvider _authenticationStateProvider;
		private readonly ILocalStorageService _localStorage;

		public AuthService(HttpClient httpClient,
											 ApiAuthenticationStateProvider authenticationStateProvider,
											 ILocalStorageService localStorage)
		{
			this._apiIdentity = "https://localhost:7290/api/Identity/";
			this._httpClient = httpClient;
			this._authenticationStateProvider = authenticationStateProvider;
			this._localStorage = localStorage;
		}

		public async Task<RegisterResult> Register(UserRegisterModel registerModel)
		{
			var response = await this._httpClient
				.PostAsJsonAsync(this.GetRequestUri(nameof(Register)), registerModel);

			var registerResult = JsonSerializer
				.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return registerResult;
		}

		public async Task<LoginResult> Login(UserLoginModel loginModel)
		{
			var loginAsJson = JsonSerializer.Serialize(loginModel);

			var response = await this._httpClient
				.PostAsync(this.GetRequestUri(nameof(Login)), new StringContent(
					loginAsJson, Encoding.UTF8, "application/json"));

			var loginResult = JsonSerializer
				.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (!response.IsSuccessStatusCode)
			{
				return loginResult;
			}

			await this._localStorage.SetItemAsync("authToken", loginResult.Token);

			var authenticationState = await this._authenticationStateProvider
				.GetAuthenticationStateAsync();

			var keyValuePair = authenticationState.User.Claims
				.FirstOrDefault(c => c.Type == "unique_name");

			var userName = keyValuePair.Value;

			this._authenticationStateProvider.MarkUserAsAuthenticated(userName);

			this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"bearer", loginResult.Token);

			return loginResult;
		}

		public async Task Logout()
		{
			await this._localStorage.RemoveItemAsync("authToken");

			this._authenticationStateProvider.MarkUserAsLoggedOut();

			this._httpClient.DefaultRequestHeaders.Authorization = null;
		}

		public async Task<UsersListModel> GetAll()
		{
			var response = await this._httpClient
				.GetFromJsonAsync<UsersListModel>(this.GetRequestUri(nameof(GetAll)));

			return response;
		}

		private string GetRequestUri(string action)
		{
			string uri = this._apiIdentity + action;
			return uri.ToLower();
		}
	}
}
