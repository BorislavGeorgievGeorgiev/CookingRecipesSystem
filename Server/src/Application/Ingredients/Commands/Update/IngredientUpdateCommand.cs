﻿using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Ingredients.Commands.Create;
using CookingRecipesSystem.Application.Ingredients.Queries.GetById;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Update
{
	public class IngredientUpdateCommand :
				IngredientRequestModel, IRequest<ApplicationResult<IngredientResponseModel>>
	{
		public class IngredientUpdateCommandHandler
					 : IRequestHandler<IngredientUpdateCommand,
						 ApplicationResult<IngredientResponseModel>>
		{
			private readonly IPhotoService _photoService;
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IAppRepository<Photo> _photoRepository;
			private readonly IMapper _mapper;

			public IngredientUpdateCommandHandler(
								IPhotoService photoService,
								IAppRepository<Ingredient> ingredientRepository,
								IAppRepository<Photo> photoRepository,
								IMapper mapper)
			{
				_photoService = photoService;
				_ingredientRepository = ingredientRepository;
				_photoRepository = photoRepository;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<IngredientResponseModel>> Handle(
				IngredientUpdateCommand request, CancellationToken cancellationToken)
			{
				var ingredient = await _ingredientRepository
										.GetAll()
										.ToAsyncEnumerable()
										.FirstOrDefaultAsync(i => i.Name == request.Name, cancellationToken);

				if (ingredient == null)
				{
					return ApplicationResult<IngredientResponseModel>
							.Failure(ExceptionMessages.IngredientInvalid);
				}

				ingredient = _mapper.Map<Ingredient>(request);

				if (request.PhotoFile != null)
				{
					PhotoResponseModel processedPhoto = await _photoService
					 .Process(request.PhotoFile, cancellationToken);

					ingredient.Photo = _mapper.Map<Photo>(processedPhoto);

					await _photoRepository.Update(ingredient.Photo);
				}

				await _ingredientRepository.Update(ingredient);

				var mappedIngredient = _mapper.Map<IngredientResponseModel>(ingredient);

				return ApplicationResult<IngredientResponseModel>.Success(mappedIngredient);
			}
		}
	}
}
