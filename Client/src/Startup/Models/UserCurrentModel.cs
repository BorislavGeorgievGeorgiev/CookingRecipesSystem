namespace CookingRecipesSystem.Startup.Models
{
	public class UserCurrentModel
	{
		public bool IsAuthenticated { get; set; }

		public string? UserName { get; set; }

		public Dictionary<string, string> Claims { get; set; }
	}
}
