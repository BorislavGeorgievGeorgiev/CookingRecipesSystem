using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Queries.AllTestEntity
{
	public class TestEntitiesQuery : IRequest<ApplicationResult<TestEntityListResponseModel>>
	{

		public class TestEntitiesQueryHandler :
			IRequestHandler<TestEntitiesQuery, ApplicationResult<TestEntityListResponseModel>>
		{
			private readonly IAppRepository<TestEntity> _testEntityRepository;
			private readonly IMapper? _mapper;

			public TestEntitiesQueryHandler(IAppRepository<TestEntity> testEntityRepository, IMapper mapper)
			{
				this._testEntityRepository = testEntityRepository;
				this._mapper = mapper;
			}

			public async Task<ApplicationResult<TestEntityListResponseModel>> Handle(
				TestEntitiesQuery request, CancellationToken cancellationToken)
			{
				var mappedResult = this._mapper!
				.ProjectTo<TestEntityResponseModel>(this._testEntityRepository.GetAllAsNoTracking())
				.ToAsyncEnumerable();

				var resultList = await mappedResult.ToListAsync(cancellationToken);
				var response = new TestEntityListResponseModel
				{
					TextList = resultList
				};

				return ApplicationResult<TestEntityListResponseModel>.Success(response);
			}
		}
	}
}
