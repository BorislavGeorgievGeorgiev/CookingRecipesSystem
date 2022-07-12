namespace CookingRecipesSystem.Application.Identity.Commands.CreateTestEntity
{
	public class TestEntityRequestModel
	{
		public TestEntityRequestModel(string text)
			=> this.Text = text;

		public string Text { get; }
	}
}
