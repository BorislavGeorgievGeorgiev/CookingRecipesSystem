namespace CookingRecipesSystem.Startup.Models
{
	public class ApiConfig
	{
		private string? _apiUrl;

		public string ApiUrl
		{
			get { return this._apiUrl!; }
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException(nameof(this.ApiUrl));
				}

				this._apiUrl = value;
			}
		}
	}
}
