namespace CookingRecipesSystem.Startup.Models
{
	public class AppResult<TResponse> where TResponse : class
	{
		public bool Succeeded { get; set; }

		public IEnumerable<string> Errors { get; set; }

		public TResponse Response { get; set; } = default!;
	}
}
