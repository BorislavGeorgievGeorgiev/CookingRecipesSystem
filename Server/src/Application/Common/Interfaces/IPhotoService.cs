using CookingRecipesSystem.Application.Common.Models;

namespace CookingRecipesSystem.Application.Common.Interfaces
{
	public interface IPhotoService
	{
		Task<PhotoResponseModel> Process(PhotoRequestModel photo);
	}
}
