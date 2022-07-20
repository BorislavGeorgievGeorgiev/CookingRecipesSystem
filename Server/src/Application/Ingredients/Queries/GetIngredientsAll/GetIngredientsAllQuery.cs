using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
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
				this._ingredientRepository = ingredientRepository;
				this._mapper = mapper;
			}

			public async Task<ApplicationResult<IngredientsListResponseModel>> Handle(
				GetIngredientsAllQuery request, CancellationToken cancellationToken)
			{
				var mappedIngredients = await this._mapper
					.ProjectTo<IngredientResponseModel>(this._ingredientRepository.GetAllAsNoTracking())
					.OrderBy(x => x.Name)
					.ToAsyncEnumerable()
					.ToListAsync(cancellationToken);

				var response = this._mapper.Map<IngredientsListResponseModel>(mappedIngredients);

				return ApplicationResult<IngredientsListResponseModel>.Success(response);
			}
		}
	}
}
