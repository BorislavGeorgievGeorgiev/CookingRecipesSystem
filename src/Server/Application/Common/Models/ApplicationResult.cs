namespace CookingRecipesSystem.Application.Common.Models
{
	public class ApplicationResult
	{
		public ApplicationResult(bool succeeded, IEnumerable<string> errors)
		{
			this.Succeeded = succeeded;
			this.Errors = errors.ToArray();
		}

		public bool Succeeded { get; }

		public IEnumerable<string> Errors { get; }

		public static ApplicationResult Success
			=> new(true, Array.Empty<string>());

		public static ApplicationResult Failure(string error)
			=> new(false, new List<string> { error });

		public static ApplicationResult Failure(IEnumerable<string> errors)
			=> new(false, errors);
	}
}