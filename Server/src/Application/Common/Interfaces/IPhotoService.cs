using CookingRecipesSystem.Application.Common.Interfaces.Lifetime;
using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IPhotoService : ITransientService
	{
		Task<PhotoResponseModel> Process(
			PhotoRequestModel photo, CancellationToken cancellationToken = default);
	}
}
