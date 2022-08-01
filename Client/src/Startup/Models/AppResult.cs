namespace CookingRecipesSystem.Startup.Models
{
	public class AppResult
	{
		public bool Succeeded { get; set; }

		public IEnumerable<string> Errors { get; set; }

		public EmptyModel Response { get; set; } = default!;
	}

	public class AppResult<TResponse> where TResponse : class
	{
		public bool Succeeded { get; set; }

		public IEnumerable<string> Errors { get; set; }

		public TResponse Response { get; set; } = default!;
	}
}
