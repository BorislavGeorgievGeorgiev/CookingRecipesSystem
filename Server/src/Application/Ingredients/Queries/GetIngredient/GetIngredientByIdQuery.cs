using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient
{
	public class GetIngredientByIdQuery : IRequest<ApplicationResult<IngredientResponseModel>>
	{
		public int Id { get; set; }

		public class GetIngredientByIdQueryHandler :
			IRequestHandler<GetIngredientByIdQuery, ApplicationResult<IngredientResponseModel>>
		{
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IMapper _mapper;

			public GetIngredientByIdQueryHandler(
				IAppRepository<Ingredient> ingredientRepository, IMapper mapper)
			{
				_ingredientRepository = ingredientRepository;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<IngredientResponseModel>> Handle(
				GetIngredientByIdQuery request, CancellationToken cancellationToken)
			{
				var ingredientQuery = _ingredientRepository
					.GetAllAsNoTracking()
					.Where(x => x.Id == request.Id);

				if (ingredientQuery.Any() == false)
				{
					return ApplicationResult<IngredientResponseModel>.Failure(
						ExceptionMessages.IngredientInvalid);
				}

				var mappedIngredient = await _mapper
					.ProjectTo<IngredientResponseModel>(ingredientQuery)
					.ToAsyncEnumerable()
					.FirstAsync(cancellationToken);

				return ApplicationResult<IngredientResponseModel>.Success(mappedIngredient);
			}
		}
	}
}
