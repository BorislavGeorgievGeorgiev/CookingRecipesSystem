using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Commands
{
	public class CreateIngredientCommand :
		IngredientRequestModel, IRequest<ApplicationResult>
	{

		public class CreateIngredientCommandHandler
			: IRequestHandler<CreateIngredientCommand, ApplicationResult>
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

			public async Task<ApplicationResult> Handle(
				CreateIngredientCommand request, CancellationToken cancellationToken)
			{
				PhotoResponseModel processedPhoto = await _photoService
					.Process(request.Photo, cancellationToken);

				var mappedPhoto = _mapper.Map<Photo>(processedPhoto);

				var photo = await _photoRepository.Create(mappedPhoto, cancellationToken);

				var mappedIngredient = _mapper.Map<Ingredient>(request);
				mappedIngredient.Photo = photo;

				await _ingredientRepository.Create(mappedIngredient, cancellationToken);

				await _ingredientRepository.SaveAsync(cancellationToken);

				return ApplicationResult.Success;
			}
		}
	}
}
