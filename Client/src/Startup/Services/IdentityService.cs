using System.Net.Http.Json;

using CookingRecipesSystem.Startup.Models;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IIdentityService
	{

		Task Login(UserLoginModel loginRequest, CancellationToken cancellationToken);
		Task Register(UserRegisterModel registerRequest, CancellationToken cancellationToken);
		Task Logout(CancellationToken cancellationToken);
		Task<UserCurrentModel> CurrentUserInfo(CancellationToken cancellationToken);
	}

	public class IdentityService : IIdentityService
	{
		private const string ApiIdentity = "https://localhost:7290/api/identity/";
		private readonly HttpClient _httpClient;

		public IdentityService(HttpClient httpClient)
		{
			this._httpClient = httpClient;
		}
		public async Task<UserCurrentModel> CurrentUserInfo(
			CancellationToken cancellationToken = default)
		{
			var result = await this._httpClient
				.GetFromJsonAsync<UserCurrentModel>(
				GetRequestUri(nameof(CurrentUserInfo)), cancellationToken);

			return result!;
		}
		public async Task Login(
			UserLoginModel loginRequest, CancellationToken cancellationToken = default)
		{
			this._httpClient.DefaultRequestHeaders.Add("mode", "no-cors");

			var result = await this._httpClient
				.PostAsJsonAsync(GetRequestUri(nameof(Login)), loginRequest, cancellationToken);

			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				throw new Exception(await result.Content.ReadAsStringAsync());
			}

			result.EnsureSuccessStatusCode();
		}
		public async Task Logout(CancellationToken cancellationToken = default)
		{
			var result = await this._httpClient
				.PostAsync(GetRequestUri(nameof(Logout)), null, cancellationToken);

			result.EnsureSuccessStatusCode();
		}
		public async Task Register(
			UserRegisterModel registerRequest, CancellationToken cancellationToken = default)
		{
			var result = await this._httpClient
				.PostAsJsonAsync(GetRequestUri(nameof(Register)), registerRequest, cancellationToken);

			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				throw new Exception(await result.Content.ReadAsStringAsync());
			}

			result.EnsureSuccessStatusCode();
		}

		private static string GetRequestUri(string action)
		{
			string uri = ApiIdentity + action;
			return uri.ToLower();
		}
	}
}
