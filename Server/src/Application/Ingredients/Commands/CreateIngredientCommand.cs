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
		public CreateIngredientCommand(
			string name, string description, PhotoRequestModel photo)
			: base(name, description, photo)
		{
		}

		public class CreateIngredientCommandHandler
			: IRequestHandler<CreateIngredientCommand, ApplicationResult>
		{
			private readonly IPhotoService _photoService;
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IAppRepository<Photo> _photoRepository;
			private readonly IMapper? _mapper;

			public CreateIngredientCommandHandler(
				IPhotoService photoService,
				IAppRepository<Ingredient> ingredientRepository,
				IAppRepository<Photo> photoRepository,
				IMapper mapper)
			{
				this._photoService = photoService;
				this._ingredientRepository = ingredientRepository;
				this._photoRepository = photoRepository;
				this._mapper = mapper;
			}

			public async Task<ApplicationResult> Handle(
				CreateIngredientCommand request, CancellationToken cancellationToken)
			{
				throw new NotImplementedException();

				//PhotoResponseModel processedPhoto = await this._photoService
				//	.Process(request.Photo, cancellationToken);

				//Photo mappedPhoto = this._mapper!.Map<Photo>(processedPhoto);

				//Photo photo = await this._photoRepository.Create(mappedPhoto, cancellationToken);

				//Ingredient mappedIngredient = this._mapper!.Map<Ingredient>(request);

				//mappedIngredient.Photo = mappedPhoto;

				//Ingredient ingredient = await this._ingredientRepository
				//	.Create(mappedIngredient, cancellationToken);

				//await this._ingredientRepository.SaveAsync(cancellationToken);

				//return ApplicationResult.Success;
			}
		}
	}
}
