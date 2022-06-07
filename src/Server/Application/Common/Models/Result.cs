namespace CookingRecipesSystem.Application.Common.Models
{
	public class Result
	{
		internal Result(bool succeeded, IEnumerable<string> errors)
		{
			this.Succeeded = succeeded;
			this.Errors = errors.ToArray();
		}

		public bool Succeeded { get; set; }

		public string[] Errors { get; set; }

		public static Result Success
				=> new(true, Array.Empty<string>());

		public static Result Failure(IEnumerable<string> errors)
				=> new(false, errors);
	}
}
