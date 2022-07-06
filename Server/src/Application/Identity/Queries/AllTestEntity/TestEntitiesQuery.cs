using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Queries.AllTestEntity
{
	public class TestEntitiesQuery : IRequest<ApplicationResult<IEnumerable<TestEntityResponseModel>>>
	{

		public class TestEntitiesQueryHandler :
			IRequestHandler<TestEntitiesQuery, ApplicationResult<IEnumerable<TestEntityResponseModel>>>
		{
			private readonly ITestEntityRepository _testEntityRepository;
			private readonly IMapper? _mapper;

			public TestEntitiesQueryHandler(ITestEntityRepository testEntityRepository, IMapper mapper)
			{
				this._testEntityRepository = testEntityRepository;
				this._mapper = mapper;
			}

			public async Task<ApplicationResult<IEnumerable<TestEntityResponseModel>>> Handle(
				TestEntitiesQuery request, CancellationToken cancellationToken)
			{
				var mappedResult = this._mapper!
				.ProjectTo<TestEntityResponseModel>(this._testEntityRepository.GetAllAsNoTracking())
				.ToAsyncEnumerable();

				var response = await mappedResult.ToListAsync(cancellationToken);

				return ApplicationResult<IEnumerable<TestEntityResponseModel>>.Success(response);
			}
		}
	}
}
