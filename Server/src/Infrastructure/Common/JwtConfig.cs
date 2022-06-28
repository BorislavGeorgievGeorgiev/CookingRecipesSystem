namespace CookingRecipesSystem.Infrastructure.Common
{
	public class JwtConfig
	{
		private string? _secret;
		private string? _expirationInMinutes;
		private string? _validIssuer;
		private string? _validAudience;

		public string Secret
		{
			get { return this._secret!; }
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException(nameof(this.Secret));
				}

				this._secret = value;
			}
		}

		public string ExpirationInMinutes
		{
			get { return this._expirationInMinutes!; }
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException(nameof(this.ExpirationInMinutes));
				}

				this._expirationInMinutes = value;
			}
		}

		public string ValidIssuer
		{
			get { return this._validIssuer!; }
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException(nameof(this.ValidIssuer));
				}

				this._validIssuer = value;
			}
		}

		public string ValidAudience
		{
			get { return this._validAudience!; }
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException(nameof(this.ValidAudience));
				}

				this._validAudience = value;
			}
		}
	}
}
