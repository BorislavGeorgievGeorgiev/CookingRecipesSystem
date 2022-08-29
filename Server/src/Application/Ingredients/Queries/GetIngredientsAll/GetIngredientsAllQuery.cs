using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetIngredientsAll
{
	public class GetIngredientsAllQuery : IRequest<ApplicationResult<IngredientsListResponseModel>>
	{
		public class GetIngredientsAllQueryHandler :
			IRequestHandler<GetIngredientsAllQuery, ApplicationResult<IngredientsListResponseModel>>
		{
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IMapper _mapper;

			public GetIngredientsAllQueryHandler(
				IAppRepository<Ingredient> ingredientRepository, IMapper mapper)
			{
				_ingredientRepository = ingredientRepository;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<IngredientsListResponseModel>> Handle(
				GetIngredientsAllQuery request, CancellationToken cancellationToken)
			{
				var allAsNoTracking = _ingredientRepository.GetAllAsNoTracking();

				var mappedIngredients = await _mapper
					.ProjectTo<IngredientResponseModel>(allAsNoTracking)
					.OrderBy(x => x.Name)
					.ToAsyncEnumerable()
					.ToListAsync(cancellationToken);

				var ingredients = _mapper.Map<IngredientsListResponseModel>(mappedIngredients);

				return ApplicationResult<IngredientsListResponseModel>.Success(ingredients);
			}
		}
	}
}
