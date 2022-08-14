using System.Text;

using CookingRecipesSystem.Startup.Models;

namespace CookingRecipesSystem.Startup.Helpers
{
	public class ConfigurationHelper
	{
		private readonly IConfiguration _configuration;
		private readonly IConfigurationSection _apiConfigSection;

		public ConfigurationHelper(IConfiguration configuration)
		{
			_configuration = configuration;
			_apiConfigSection = _configuration.GetSection(nameof(ApiConfig));
		}

		public string GetRequestUri(string controllerName, string action)
		{
			var sb = new StringBuilder();
			var apiUrl = _apiConfigSection.GetSection(nameof(ApiConfig.ApiUrl)).Value;
			sb.Append(apiUrl)
				.Append(controllerName)
				.Append('/')
				.Append(action);

			return sb.ToString().ToLower();
		}
	}
}
