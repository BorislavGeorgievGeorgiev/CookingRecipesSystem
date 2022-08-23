namespace CookingRecipesSystem.Startup.Models
{
	public class ApiConfig
	{
		private string? _apiUrl;

		public string ApiUrl
		{
			get { return _apiUrl!; }
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException(nameof(ApiUrl));
				}

				_apiUrl = value;
			}
		}
	}
}
