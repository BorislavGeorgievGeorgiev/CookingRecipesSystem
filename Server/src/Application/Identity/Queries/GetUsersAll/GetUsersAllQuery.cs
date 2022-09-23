using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Identity.Queries.GetUserById;

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
				_userManagerService = userManagerService;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<UsersListResponseModel>> Handle(
				GetUsersAllQuery request, CancellationToken cancellationToken)
			{
				var mappedUsers = await _mapper
					.ProjectTo<UserSimpleResponseModel>(_userManagerService.GetAllAsNoTracking().Response)
					.OrderBy(x => x.UserName)
					.ToAsyncEnumerable()
					.ToListAsync(cancellationToken);

				var mappedUserList = _mapper.Map<UsersListResponseModel>(mappedUsers);

				return ApplicationResult<UsersListResponseModel>.Success(mappedUserList);
			}
		}
	}
}
