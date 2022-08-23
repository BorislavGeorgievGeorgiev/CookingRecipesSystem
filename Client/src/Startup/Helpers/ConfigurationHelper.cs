using System.Text;

using CookingRecipesSystem.Startup.Models;

namespace CookingRecipesSystem.Startup.Helpers
{
	public class ConfigurationHelper
	{
		private readonly IConfiguration _configuration;
		private readonly ApiConfig _apiConfigSection;

		public ConfigurationHelper(IConfiguration configuration)
		{
			_configuration = configuration;
			_apiConfigSection = new ApiConfig();
			_configuration.GetSection(nameof(ApiConfig))
				.Bind(_apiConfigSection, o => o.BindNonPublicProperties = true);
		}

		public string GetRequestUri(string controllerName, string action)
		{
			var sb = new StringBuilder();
			var apiUrl = _apiConfigSection.ApiUrl;
			sb.Append(apiUrl)
				.Append(controllerName)
				.Append('/')
				.Append(action);

			return sb.ToString().ToLower();
		}
	}
}
