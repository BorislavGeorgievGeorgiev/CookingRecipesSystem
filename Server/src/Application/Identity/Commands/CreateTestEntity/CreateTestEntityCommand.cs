using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using CookingRecipesSystem.Domain.Entities;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.CreateTestEntity
{
	public class CreateTestEntityCommand : TestEntityRequestModel, IRequest<ApplicationResult>
	{
		public CreateTestEntityCommand(string text)
			: base(text)
		{
		}

		public class CreateTestEntityCommandHandler
			: IRequestHandler<CreateTestEntityCommand, ApplicationResult>
		{
			private readonly IAppRepository<TestEntity> _testEntityRepository;

			public CreateTestEntityCommandHandler(IAppRepository<TestEntity> testEntityRepository)
			{
				this._testEntityRepository = testEntityRepository;
			}

			public async Task<ApplicationResult> Handle(
				CreateTestEntityCommand request, CancellationToken cancellationToken)
			{
				var entity = new TestEntity
				{
					Text = request.Text,
				};

				await this._testEntityRepository.Create(entity, cancellationToken);

				await this._testEntityRepository.SaveAsync(cancellationToken);

				return ApplicationResult.Success;
			}
		}
	}
}
