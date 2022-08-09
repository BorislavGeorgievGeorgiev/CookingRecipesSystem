namespace CookingRecipesSystem.Startup.Models
{
	public class AppResult
	{
		public AppResult(bool succeeded, IEnumerable<string> errors)
		{
			this.Succeeded = succeeded;
			this.Errors = errors.ToArray();
		}

		public bool Succeeded { get; } = false;

		public IEnumerable<string> Errors { get; }

		public EmptyObject Response => default!;

		public static AppResult Success
			=> new(true, Array.Empty<string>());

		public static AppResult Failure(string error)
			=> Failure(new string[] { error });

		public static AppResult Failure(IEnumerable<string> errors)
			=> new(false, errors);
	}

	public class AppResult<TResponse> : AppResult
		where TResponse : class
	{
		private readonly TResponse _response;

		public AppResult(TResponse response, bool succeeded, IEnumerable<string> errors)
			: base(succeeded, errors)
		{
			this._response = response;
		}

		public new TResponse Response
			=> this.Succeeded ? this._response : default!;

		public static new AppResult<TResponse> Success(TResponse response)
			=> new(response, true, Array.Empty<string>());

		public static new AppResult<TResponse> Failure(string error)
			=> Failure(new string[] { error });

		public static new AppResult<TResponse> Failure(IEnumerable<string> errors)
			=> new(default!, false, errors);
	}
}
