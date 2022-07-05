using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Queries
{
	public class TestEntitiesQuery : IRequest<ApplicationResult<IEnumerable<TestEntityResponseModel>>>
	{

		public class TestEntitiesQueryHandler :
			IRequestHandler<TestEntitiesQuery, ApplicationResult<IEnumerable<TestEntityResponseModel>>>
		{
			private readonly ITestEntityRepository _testEntityRepository;
			public TestEntitiesQueryHandler(ITestEntityRepository testEntityRepository)
			{
				this._testEntityRepository = testEntityRepository;
			}

			public async Task<ApplicationResult<IEnumerable<TestEntityResponseModel>>> Handle(
				TestEntitiesQuery request, CancellationToken cancellationToken)
				=> await this._testEntityRepository.All(cancellationToken);
		}
	}
}
