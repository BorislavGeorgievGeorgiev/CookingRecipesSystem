using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using CookingRecipesSystem.Startup.Models;

using Microsoft.AspNetCore.Components;

namespace CookingRecipesSystem.Startup.Services
{
	public interface IHttpService
	{
		Task<T> Get<T>(string uri);
		Task<T> Post<T>(string uri, object value);
	}

	public class HttpService : IHttpService
	{
		private HttpClient _httpClient;
		private NavigationManager _navigationManager;
		private ILocalStorageService _localStorageService;
		private IConfiguration _configuration;

		public HttpService(
				HttpClient httpClient,
				NavigationManager navigationManager,
				ILocalStorageService localStorageService,
				IConfiguration configuration
		)
		{
			this._httpClient = httpClient;
			this._navigationManager = navigationManager;
			this._localStorageService = localStorageService;
			this._configuration = configuration;
		}

		public async Task<T> Get<T>(string uri)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, uri);
			return await this.sendRequest<T>(request);
		}

		public async Task<T> Post<T>(string uri, object value)
		{
			var request = new HttpRequestMessage(HttpMethod.Post, uri);
			request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
			return await this.sendRequest<T>(request);
		}

		// helper methods

		private async Task<T> sendRequest<T>(HttpRequestMessage request)
		{
			// add jwt auth header if user is logged in and request is to the api url
			var user = await this._localStorageService.GetItem<User>("user");
			var isApiUrl = !request.RequestUri.IsAbsoluteUri;
			if (user != null && isApiUrl)
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
			}

			using var response = await this._httpClient.SendAsync(request);

			// auto logout on 401 response
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				this._navigationManager.NavigateTo("logout");
				return default;
			}

			// throw exception on error response
			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
				throw new Exception(error["message"]);
			}

			return await response.Content.ReadFromJsonAsync<T>();
		}
	}
}
