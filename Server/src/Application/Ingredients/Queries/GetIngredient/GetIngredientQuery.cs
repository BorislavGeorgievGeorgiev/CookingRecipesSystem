using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient
{
	public class GetIngredientQuery : IRequest<ApplicationResult<IngredientResponseModel>>
	{
		public int Id { get; set; }

		public class GetIngredientQueryHandler :
			IRequestHandler<GetIngredientQuery, ApplicationResult<IngredientResponseModel>>
		{
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IMapper _mapper;

			public GetIngredientQueryHandler(
				IAppRepository<Ingredient> ingredientRepository, IMapper mapper)
			{
				_ingredientRepository = ingredientRepository;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<IngredientResponseModel>> Handle(
				GetIngredientQuery request, CancellationToken cancellationToken)
			{
				var ingredientQuery = _ingredientRepository
					.GetAllAsNoTracking()
					.Where(x => x.Id == request.Id);

				if (ingredientQuery.Any() == false)
				{
					return ApplicationResult<IngredientResponseModel>.Failure(
						ExceptionMessages.InvalidIngredient);
				}

				var ingredient = await _mapper
					.ProjectTo<IngredientResponseModel>(ingredientQuery)
					.ToAsyncEnumerable()
					.FirstAsync();

				return ApplicationResult<IngredientResponseModel>.Success(ingredient);
			}
		}
	}
}
