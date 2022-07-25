using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Queries.GetUsersAll
{
	public class GetUsersAllQuery : IRequest<ApplicationResult<UsersListResponseModel>>
	{
		public class GetUsersAllQueryHandler :
			IRequestHandler<GetUsersAllQuery, ApplicationResult<UsersListResponseModel>>
		{
			private readonly IUserManagerService _userManagerService;
			private readonly IMapper _mapper;

			public GetUsersAllQueryHandler(
				IUserManagerService userManagerService, IMapper mapper)
			{
				this._userManagerService = userManagerService;
				this._mapper = mapper;
			}

			public async Task<ApplicationResult<UsersListResponseModel>> Handle(
				GetUsersAllQuery request, CancellationToken cancellationToken)
			{
				var mappedUsers = await this._mapper
					.ProjectTo<UserResponseModel>(this._userManagerService.GetAllAsNoTracking().Response)
					.OrderBy(x => x.UserName)
					.ToAsyncEnumerable()
					.ToListAsync(cancellationToken);

				var response = this._mapper.Map<UsersListResponseModel>(mappedUsers);

				return ApplicationResult<UsersListResponseModel>.Success(response);
			}
		}
	}
}
