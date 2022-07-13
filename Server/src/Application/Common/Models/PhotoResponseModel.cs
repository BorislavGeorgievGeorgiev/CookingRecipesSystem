namespace CookingRecipesSystem.Application.Common.Models
{
	public class PhotoResponseModel
	{
		public PhotoResponseModel(byte[] original, byte[] thumbnail)
		{
			this.Original = original;
			this.Thumbnail = thumbnail;
		}

		public byte[] Original { get; }

		public byte[] Thumbnail { get; }
	}
}
