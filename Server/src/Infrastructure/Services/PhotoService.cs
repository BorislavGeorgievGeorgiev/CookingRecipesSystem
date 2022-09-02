using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using Microsoft.AspNetCore.Http;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CookingRecipesSystem.Infrastructure.Services
{
	public class PhotoService : IPhotoService
	{
		private const int MainPhotoWidth = 900;
		private const int CardPhotoWidth = 300;
		private const int ThumbnailWidth = 100;

		public async Task<PhotoResponseModel> Process(
			IFormFile photo, CancellationToken cancellationToken = default)
		{
			using var imageResult = await Image.LoadAsync(
				photo.OpenReadStream(), cancellationToken);

			var mainPhoto = await SaveImage(imageResult, MainPhotoWidth, false);
			var phonePhoto = await SaveImage(imageResult, CardPhotoWidth);
			var thumbnail = await SaveImage(imageResult, ThumbnailWidth);

			return new PhotoResponseModel
			{
				MainPhoto = mainPhoto,
				CardPhoto = phonePhoto,
				Thumbnail = thumbnail
			};
		}

		private async Task<byte[]> SaveImage(
			Image image, int resizeSize, bool isSquare = true)
		{
			//TODO: Refactor to fit for all ratios.
			int width = image.Width;
			int height = image.Height;
			bool isRotated = false;

			if (image.Width > image.Height && isSquare)
			{
				image.Mutate(i => i.Rotate(-90));
				width = image.Width;
				height = image.Height;
				isRotated = true;
			}

			if (width != resizeSize)
			{
				height = (int)((double)resizeSize / width * height);
				width = resizeSize;
			}

			image.Mutate(i => i.Resize(new Size(width, height)));

			if (height > resizeSize)
			{
				var halfHeightToCrop = (height - resizeSize) / 2;
				image.Mutate(i => i.Rotate(-90));
				image.Mutate(i => i.Crop(resizeSize + halfHeightToCrop, width));
				image.Mutate(i => i.Rotate(180));
				image.Mutate(i => i.Crop(resizeSize, width));
				image.Mutate(i => i.Rotate(-90));
			}

			if (isRotated && isSquare)
			{
				image.Mutate(i => i.Rotate(90));
			}

			image.Metadata.ExifProfile = null;

			await using var memoryStream = new MemoryStream();

			await image.SaveAsJpegAsync(memoryStream);

			return memoryStream.ToArray();
		}
	}
}
