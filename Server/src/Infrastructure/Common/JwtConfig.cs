namespace CookingRecipesSystem.Infrastructure.Common
{
	public class JwtConfig
	{
		private string? _secret;
		private string? _expirationInMinutes;

		public string Secret
		{
			get { return this._secret!; }
			private set
			{
				this._secret = value ?? throw
					new ArgumentNullException(nameof(this.Secret));
			}
		}

		public string ExpirationInMinutes
		{
			get { return this._expirationInMinutes!; }
			private set
			{
				this._expirationInMinutes = value ?? throw
					new ArgumentNullException(nameof(this.ExpirationInMinutes));
			}
		}
	}
}
