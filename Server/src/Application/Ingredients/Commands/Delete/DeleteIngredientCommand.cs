using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Delete
{
	public class DeleteIngredientCommand : EntityKeyModel, IRequest<ApplicationResult>
	{
		public class DeleteIngredientCommandHandler
					 : IRequestHandler<DeleteIngredientCommand, ApplicationResult>
		{
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IAppRepository<Photo> _photoRepository;

			public DeleteIngredientCommandHandler(
				IAppRepository<Ingredient> ingredientRepository,
				IAppRepository<Photo> photoRepository)
			{
				_ingredientRepository = ingredientRepository;
				_photoRepository = photoRepository;
			}

			public async Task<ApplicationResult> Handle(
				DeleteIngredientCommand request, CancellationToken cancellationToken)
			{
				var ingredient = await _ingredientRepository.GetAll(nameof(Photo))
					.Where(x => x.Id == request.Id)
					.ToAsyncEnumerable()
					.FirstOrDefaultAsync(cancellationToken);

				if (ingredient == null)
				{
					return ApplicationResult.Failure(
						ExceptionMessages.IngredientInvalid);
				}

				var isIngredientDeleted = await _ingredientRepository
					.DeleteNoPermanent(ingredient, cancellationToken);

				if (isIngredientDeleted == false)
				{
					return ApplicationResult.Failure(ExceptionMessages.IngredientNotDeleted);
				}

				var photo = await _photoRepository.GetAll()
					.ToAsyncEnumerable()
					.FirstOrDefaultAsync(ph => ph.Id == ingredient.Photo.Id);

				var isPhotoDeleted = await _photoRepository
					.DeleteNoPermanent(ingredient.Photo, cancellationToken);

				if (isPhotoDeleted == false)
				{
					return ApplicationResult.Failure(ExceptionMessages.IngredientPhotoNotDeleted);
				}

				await _ingredientRepository.SaveAsync(cancellationToken);

				return ApplicationResult.Success;
			}
		}
	}
}
