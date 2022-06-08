namespace CookingRecipesSystem.Application.Common.Models
{
	public class ApplicationResult
	{
		internal ApplicationResult(bool succeeded, IEnumerable<string> errors)
		{
			this.Succeeded = succeeded;
			this.Errors = errors.ToArray();
		}

		public bool Succeeded { get; protected set; }

		public IEnumerable<string> Errors { get; set; }

		public static ApplicationResult Success
			=> new(true, Array.Empty<string>());

		public static ApplicationResult Failure(IEnumerable<string> errors)
			=> new(false, errors);
	}
}
