﻿using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Queries.GetById;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Queries.Search
{
	public class IngredientSearchQuery :
		PaginationModel,
		IRequest<ApplicationResult<IEnumerable<IngredientResponseModel>>>
	{
		public string Name { get; set; } = string.Empty;

		public class IngredientSearchQueryHandler :
			IRequestHandler<IngredientSearchQuery, ApplicationResult<IEnumerable<IngredientResponseModel>>>
		{
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IMapper _mapper;

			public IngredientSearchQueryHandler(
				IAppRepository<Ingredient> ingredientRepository,
				IMapper mapper)
			{
				_ingredientRepository = ingredientRepository;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<IEnumerable<IngredientResponseModel>>> Handle(
				IngredientSearchQuery request, CancellationToken cancellationToken)
			{
				var allAsNoTrackingQueryable = _ingredientRepository.GetAllAsNoTracking();

				var mappedIngredients = await _mapper
					.ProjectTo<IngredientResponseModel>(allAsNoTrackingQueryable)
					.Where(i =>
					i.Name.ToLower() == request.Name.ToLower() ||
					i.Name.ToLower().Contains(request.Name.ToLower()))
					.OrderBy(i => i.Name)
					.Skip(request.Skip)
					.Take(request.Take)
					.ToAsyncEnumerable()
					.ToListAsync();

				var ingredients = _mapper.Map<IEnumerable<IngredientResponseModel>>(mappedIngredients);

				return ApplicationResult<IEnumerable<IngredientResponseModel>>.Success(ingredients);
			}
		}
	}
}
