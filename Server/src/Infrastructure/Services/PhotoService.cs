using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using Microsoft.AspNetCore.Http;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CookingRecipesSystem.Infrastructure.Services
{
	public class PhotoService : IPhotoService
	{
		private const int MainPhotoWidth = 1280;
		private const int MainPhotoHeight = 720;
		private const int PhonePhotoWidth = 320;
		private const int PhonePhotoHeight = 240;
		private const int ThumbnailWidth = 100;
		private const int ThumbnailHeight = 100;

		public async Task<PhotoResponseModel> Process(
			IFormFile photo, CancellationToken cancellationToken = default)
		{
			using var imageResult = await Image.LoadAsync(photo.OpenReadStream(), cancellationToken);

			var mainPhoto = await SaveImage(imageResult, MainPhotoWidth, MainPhotoHeight);
			var phonePhoto = await SaveImage(imageResult, PhonePhotoWidth, PhonePhotoHeight);
			var thumbnail = await SaveImage(imageResult, ThumbnailWidth, ThumbnailHeight);

			return new PhotoResponseModel
			{
				MainPhoto = mainPhoto,
				PhonePhoto = phonePhoto,
				Thumbnail = thumbnail
			};
		}

		private async Task<byte[]> SaveImage(Image image, int resizeWidth, int resizeHeight)
		{
			int width = image.Width;
			int height = image.Height;

			if (width > resizeWidth)
			{
				height = (int)((double)resizeWidth / width * height);
				width = resizeWidth;
			}

			image.Mutate(i => i.Resize(new Size(width, height)));

			image.Metadata.ExifProfile = null;

			await using var memoryStream = new MemoryStream();

			await image.SaveAsJpegAsync(memoryStream);

			return memoryStream.ToArray();
		}
	}
}
