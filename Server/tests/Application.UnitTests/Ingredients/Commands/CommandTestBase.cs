using System.Text;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.UnitTests.Ingredients.Commands
{
	public class CommandTestBase
	{
		protected IFormFile GetIFormImageFile()
		{
			string content = "File content.";
			byte[] bytes = Encoding.Unicode.GetBytes(content);
			var iFormFileFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpeg");

			return iFormFileFile;
		}
	}
}
