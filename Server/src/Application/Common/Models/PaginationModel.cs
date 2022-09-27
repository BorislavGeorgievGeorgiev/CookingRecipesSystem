using CookingRecipesSystem.Domain.Common.Constants;

namespace CookingRecipesSystem.Application.Common.Models
{
	public class PaginationModel
	{
		public int Skip { get; set; } = AppConstants.SkipDefault;

		public int Take { get; set; } = AppConstants.TakeDefault;
	}
}
