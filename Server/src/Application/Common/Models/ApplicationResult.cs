namespace CookingRecipesSystem.Application.Common.Models
{
	public class ApplicationResult
	{
		public ApplicationResult(bool succeeded, IEnumerable<string> errors)
		{
			this.Succeeded = succeeded;
			this.Errors = errors.ToArray();
		}

		public bool Succeeded { get; } = false;

		public IEnumerable<string> Errors { get; }

		public EmptyObject Response => default!;

		public static ApplicationResult Success
			=> new(true, Array.Empty<string>());

		public static ApplicationResult Failure(string error)
			=> Failure(new string[] { error });

		public static ApplicationResult Failure(IEnumerable<string> errors)
			=> new(false, errors);
	}

	public class ApplicationResult<TResponse> : ApplicationResult
		where TResponse : class
	{
		private readonly TResponse _response;

		public ApplicationResult(TResponse response, bool succeeded, IEnumerable<string> errors)
			: base(succeeded, errors)
		{
			this._response = response;
		}

		public new TResponse Response
			=> this.Succeeded ? this._response : default!;

		public static new ApplicationResult<TResponse> Success(TResponse response)
			=> new(response, true, Array.Empty<string>());

		public static new ApplicationResult<TResponse> Failure(string error)
			=> Failure(new string[] { error });

		public static new ApplicationResult<TResponse> Failure(IEnumerable<string> errors)
			=> new(default!, false, errors);
	}
}