using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Queries.GetAll;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Queries.Search
{
	public class IngredientSearchQuery : IRequest<ApplicationResult<IngredientListResponseModel>>
	{
		public class IngredientSearchQueryHandler :
			IRequestHandler<IngredientSearchQuery, ApplicationResult<IngredientListResponseModel>>
		{
			public Task<ApplicationResult<IngredientListResponseModel>> Handle(
				IngredientSearchQuery request, CancellationToken cancellationToken)
			{
				throw new NotImplementedException();
			}
		}
	}
}
