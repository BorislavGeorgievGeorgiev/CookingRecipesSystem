
using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Entities;
using CookingRecipesSystem.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace CookingRecipesSystem.Infrastructure.Repositories
{
	public class TestEntityRepository
		: ApplicationData<CookingRecipesSystemDbContext, TestEntity>, ITestEntityRepository
	{
		private readonly IMapper? _mapper;

		public TestEntityRepository(CookingRecipesSystemDbContext dbData, IMapper mapper)
			: base(dbData)
		{
			this._mapper = mapper;
		}

		public async Task<ApplicationResult<IEnumerable<TestEntityResponseModel>>> All(
			CancellationToken cancellationToken = default)
		{
			var result = await this._mapper!
				.ProjectTo<TestEntityResponseModel>(this.GetAllNoTracking())
				.ToListAsync(cancellationToken);

			return ApplicationResult<IEnumerable<TestEntityResponseModel>>.Success(result);
		}
	}
}
