using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Application.Recipes.Commands.Create
{
	public class RecipeCreateCommand :
				RecipeRequestModel, IRequest<ApplicationResult<EntityKeyModel>>
	{

		public class RecipeCreateCommandHandler
				: IRequestHandler<RecipeCreateCommand, ApplicationResult<EntityKeyModel>>
		{
			private readonly IAppRepository<Recipe> _recipeRepository;
			private readonly IAppRepository<Photo> _photoRepository;
			private readonly IAppRepository<RecipeTask> _recipeTaskRepository;
			private readonly IPhotoService _photoService;
			private readonly IMapper _mapper;

			public RecipeCreateCommandHandler(
				IAppRepository<Recipe> recipeRepository,
				IAppRepository<Photo> photoRepository,
				IAppRepository<RecipeTask> recipeTaskRepository,
				IPhotoService photoService,
				IMapper mapper)
			{
				_recipeRepository = recipeRepository;
				_photoRepository = photoRepository;
				_photoService = photoService;
				_recipeTaskRepository = recipeTaskRepository;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<EntityKeyModel>> Handle(
				RecipeCreateCommand request, CancellationToken cancellationToken)
			{
				var recipe = await _recipeRepository
						.GetAllAsNoTracking()
						.ToAsyncEnumerable()
						.FirstOrDefaultAsync(i => i.Title.ToLower() == request.Title.ToLower(), cancellationToken);

				if (recipe != null)
				{
					return ApplicationResult<EntityKeyModel>
							.Failure(ExceptionMessages.RecipeExist);
				}

				var recipePhoto = await CreatePhoto(request.Photo, cancellationToken);
				var mappedRecipe = _mapper.Map<Recipe>(request);
				mappedRecipe.Photo = recipePhoto;
				recipe = await _recipeRepository.Create(mappedRecipe);

				foreach (var requestRecipeTask in request.RecipeTasks)
				{
					var recipeTaskPhoto = await CreatePhoto(requestRecipeTask.Photo, cancellationToken);
					var mappedRecipeTask = _mapper.Map<RecipeTask>(requestRecipeTask);
					mappedRecipeTask.Photo = recipeTaskPhoto;
					var recipeTask = await _recipeTaskRepository.Create(mappedRecipeTask);
					recipe.RecipeTasks.Add(recipeTask);
				}

				var writtenEntries = await _recipeRepository.SaveAsync(cancellationToken);

				if (writtenEntries == 0)
				{
					return ApplicationResult<EntityKeyModel>.Failure(ExceptionMessages.RecipeNotCreated);
				}

				return ApplicationResult<EntityKeyModel>.Success(new EntityKeyModel { Id = recipe.Id });
			}

			private async Task<Photo> CreatePhoto(IFormFile requestPhoto, CancellationToken cancellationToken)
			{
				var processedPhoto = await _photoService.Process(requestPhoto, cancellationToken);
				var mappedPhoto = _mapper.Map<Photo>(processedPhoto);
				var photo = await _photoRepository.Create(mappedPhoto, cancellationToken);

				return photo;
			}
		}
	}
}
