﻿using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Queries.GetIngredient;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Queries.GetAll
{
	public class IngredientGetAllQuery : IRequest<ApplicationResult<IngredientListResponseModel>>
	{
		public class IngredientGetAllQueryHandler :
			IRequestHandler<IngredientGetAllQuery, ApplicationResult<IngredientListResponseModel>>
		{
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IMapper _mapper;

			public IngredientGetAllQueryHandler(
				IAppRepository<Ingredient> ingredientRepository, IMapper mapper)
			{
				_ingredientRepository = ingredientRepository;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<IngredientListResponseModel>> Handle(
				IngredientGetAllQuery request, CancellationToken cancellationToken)
			{
				var allAsNoTracking = _ingredientRepository.GetAllAsNoTracking();

				var mappedIngredients = await _mapper
					.ProjectTo<IngredientResponseModel>(allAsNoTracking)
					.OrderBy(x => x.Name)
					.ToAsyncEnumerable()
					.ToListAsync(cancellationToken);

				var ingredients = _mapper.Map<IngredientListResponseModel>(mappedIngredients);

				return ApplicationResult<IngredientListResponseModel>.Success(ingredients);
			}
		}
	}
}