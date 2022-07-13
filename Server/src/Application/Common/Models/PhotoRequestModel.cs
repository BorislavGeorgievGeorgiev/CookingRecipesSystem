namespace CookingRecipesSystem.Application.Common.Models
{
	public class PhotoRequestModel
	{
		public PhotoRequestModel(Stream content) => this.Content = content;

		public Stream Content { get; }
	}
}
