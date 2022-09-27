using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Ingredients.Commands.Delete
{
	public class IngredientDeleteCommand : EntityKeyModel, IRequest<ApplicationResult>
	{
		public class IngredientDeleteCommandHandler
					 : IRequestHandler<IngredientDeleteCommand, ApplicationResult>
		{
			private readonly IAppRepository<Ingredient> _ingredientRepository;
			private readonly IAppRepository<Photo> _photoRepository;

			public IngredientDeleteCommandHandler(
				IAppRepository<Ingredient> ingredientRepository,
				IAppRepository<Photo> photoRepository)
			{
				_ingredientRepository = ingredientRepository;
				_photoRepository = photoRepository;
			}

			public async Task<ApplicationResult> Handle(
				IngredientDeleteCommand request, CancellationToken cancellationToken)
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

				await _ingredientRepository.DeleteNoPermanent(ingredient);

				var photo = await _photoRepository.GetAll()
					.ToAsyncEnumerable()
					.FirstOrDefaultAsync(ph => ph.Id == ingredient.Photo.Id);

				await _photoRepository.DeleteNoPermanent(ingredient.Photo);

				await _ingredientRepository.SaveAsync(cancellationToken);

				return ApplicationResult.Success;
			}
		}
	}
}
