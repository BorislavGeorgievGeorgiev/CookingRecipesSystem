namespace CookingRecipesSystem.Application.Common.Models
{
	public class TestEntityRequestModel
	{
		public TestEntityRequestModel(string text)
			=> this.Text = text;

		public string Text { get; }
	}
}
