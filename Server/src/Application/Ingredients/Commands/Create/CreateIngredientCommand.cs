using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Create
{
  public class CreateIngredientCommand :
        IngredientRequestModel, IRequest<ApplicationResult<EntityKeyModel>>
  {

    public class CreateIngredientCommandHandler
        : IRequestHandler<CreateIngredientCommand, ApplicationResult<EntityKeyModel>>
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

      public async Task<ApplicationResult<EntityKeyModel>> Handle(
          CreateIngredientCommand request, CancellationToken cancellationToken)
      {
        var ingredient = await _ingredientRepository
            .GetAll()
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync(i => i.Name == request.Name);

        if (ingredient != null)
        {
          return ApplicationResult<EntityKeyModel>
              .Failure(ExceptionMessages.IngredientExist);
        }

        PhotoResponseModel processedPhoto = await _photoService
            .Process(request.Photo, cancellationToken);

        var mappedPhoto = _mapper.Map<Photo>(processedPhoto);

        var photo = await _photoRepository.Create(mappedPhoto, cancellationToken);

        var mappedIngredient = _mapper.Map<Ingredient>(request);
        mappedIngredient.Photo = photo;

        ingredient = await _ingredientRepository
            .Create(mappedIngredient, cancellationToken);

        await _ingredientRepository.SaveAsync(cancellationToken);

        return ApplicationResult<EntityKeyModel>
            .Success(new EntityKeyModel { Id = ingredient.Id });
      }
    }
  }
}
