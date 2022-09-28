using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Recipes.Commands.Create
{
	public class RecipeCreateCommand :
				RecipeRequestModel, IRequest<ApplicationResult<EntityKeyModel>>
	{

		/*public class RecipeCreateCommandHandler
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
						.FirstOrDefaultAsync(i => i.Title == request.Title, cancellationToken);

				if (recipe != null)
				{
					return ApplicationResult<EntityKeyModel>
							.Failure(ExceptionMessages.RcipeExist);
				}

				throw new NotImplementedException();
			}
		}*/
	}
}
