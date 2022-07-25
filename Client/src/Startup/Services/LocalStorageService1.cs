using System.Text.Json;

using Microsoft.JSInterop;

namespace CookingRecipesSystem.Startup.Services
{
	public interface ILocalStorageService1
	{
		Task<T> GetItem<T>(string key);
		Task SetItem<T>(string key, T value);
		Task RemoveItem(string key);
	}

	public class LocalStorageService1 : ILocalStorageService1
	{
		private IJSRuntime _jsRuntime;

		public LocalStorageService1(IJSRuntime jsRuntime)
		{
			this._jsRuntime = jsRuntime;
		}

		public async Task<T> GetItem<T>(string key)
		{
			var json = await this._jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

			if (json == null)
			{
				return default!;
			}

			return JsonSerializer.Deserialize<T>(json)!;
		}

		public async Task SetItem<T>(string key, T value)
		{
			await this._jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
		}

		public async Task RemoveItem(string key)
		{
			await this._jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
		}
	}
}
