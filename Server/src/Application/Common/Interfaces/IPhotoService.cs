using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Common.Models;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IPhotoService : ITransientService
	{
		Task<PhotoResponseModel> Process(
			IFormFile photo, CancellationToken cancellationToken = default);
	}
}
