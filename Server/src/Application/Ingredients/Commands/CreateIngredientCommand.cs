using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Commands
{
	public class CreateIngredientCommand :
		IngredientRequestModel, IRequest<ApplicationResult<EntityKeyResponseModel>>
	{

		public class CreateIngredientCommandHandler
			: IRequestHandler<CreateIngredientCommand, ApplicationResult<EntityKeyResponseModel>>
		{
			private readonly IPhotoService _photoService;
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IAppRepository<Photo> _photoRepository;
			private readonly IMapper _mapper;

			public CreateIngredientCommandHandler(
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

			public async Task<ApplicationResult<EntityKeyResponseModel>> Handle(
				CreateIngredientCommand request, CancellationToken cancellationToken)
			{
				var isExist = _ingredientRepository
					.GetAll()
					.FirstOrDefault(i => i.Name == request.Name) != null;

				if (isExist)
				{
					return ApplicationResult<EntityKeyResponseModel>
						.Failure(ExceptionMessages.IngredientExist);
				}

				PhotoResponseModel processedPhoto = await _photoService
					.Process(request.Photo, cancellationToken);

				var mappedPhoto = _mapper.Map<Photo>(processedPhoto);

				var photo = await _photoRepository.Create(mappedPhoto, cancellationToken);

				var mappedIngredient = _mapper.Map<Ingredient>(request);
				mappedIngredient.Photo = photo;

				var ingredient = await _ingredientRepository
					.Create(mappedIngredient, cancellationToken);

				await _ingredientRepository.SaveAsync(cancellationToken);

				return ApplicationResult<EntityKeyResponseModel>
					.Success(new EntityKeyResponseModel { Id = ingredient.Id });
			}
		}
	}
}
