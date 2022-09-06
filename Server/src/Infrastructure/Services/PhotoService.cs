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
		private const int CardPhotoWidth = 300;
		private const int ThumbnailWidth = 100;

		public async Task<PhotoResponseModel> Process(
			IFormFile photo, CancellationToken cancellationToken = default)
		{
			using var imageResult = await Image.LoadAsync(
				photo.OpenReadStream(), cancellationToken);

			var mainPhoto = await SaveImage(imageResult, MainPhotoWidth, MainPhotoHeight);
			var phonePhoto = await SaveImage(imageResult, CardPhotoWidth, CardPhotoWidth);
			var thumbnail = await SaveImage(imageResult, ThumbnailWidth, ThumbnailWidth);

			return new PhotoResponseModel
			{
				MainPhoto = mainPhoto,
				CardPhoto = phonePhoto,
				Thumbnail = thumbnail
			};
		}

		private static async Task<byte[]> SaveImage(
			Image image, int resizeWidth, int resizeHeight)
		{
			int width = image.Width;
			int height = image.Height;
			bool isRotated = false;

			var resizeRatio = (decimal)resizeWidth / resizeHeight;
			var ratio = (decimal)width / height;
			var rotatedImageRatio = (decimal)height / width;

			if (resizeRatio < 1)
			{
				resizeRatio = (decimal)resizeHeight / resizeWidth;
			}

			if ((ratio > resizeRatio) ||
				(ratio < 1 && rotatedImageRatio <= resizeRatio))
			{
				image.Mutate(i => i.Rotate(-90));
				width = image.Width;
				height = image.Height;
				isRotated = true;
			}

			if (image.Width != resizeWidth)
			{
				if ((ratio > resizeRatio) || (rotatedImageRatio > resizeRatio))
				{
					height = (int)((double)resizeHeight / width * height);
					width = resizeHeight;
				}
				else
				{
					height = (int)((double)resizeWidth / width * height);
					width = resizeWidth;
				}
			}

			image.Mutate(i => i.Resize(new Size(width, height)));

			if (isRotated || rotatedImageRatio > resizeRatio)
			{
				image.Mutate(i => i.Rotate(90));
			}

			var x = (image.Width - resizeWidth) / 2;
			var y = (image.Height - resizeHeight) / 2;
			image.Mutate(i => i.Crop(new Rectangle(x, y, resizeWidth, resizeHeight)));

			image.Metadata.ExifProfile = null;

			await using var memoryStream = new MemoryStream();

			await image.SaveAsJpegAsync(memoryStream);

			return memoryStream.ToArray();
		}

	}
}
