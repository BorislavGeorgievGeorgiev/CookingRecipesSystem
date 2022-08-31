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
				_userManagerService = userManagerService;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<UsersListResponseModel>> Handle(
				GetUsersAllQuery request, CancellationToken cancellationToken)
			{
				var mappedUsers = await _mapper
					.ProjectTo<UserResponseModel>(_userManagerService.GetAllAsNoTracking().Response)
					.OrderBy(x => x.UserName)
					.ToAsyncEnumerable()
					.ToListAsync(cancellationToken);

				var response = _mapper.Map<UsersListResponseModel>(mappedUsers);

				return ApplicationResult<UsersListResponseModel>.Success(response);
			}
		}
	}
}
