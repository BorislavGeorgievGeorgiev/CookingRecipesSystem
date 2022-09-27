using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Identity.Queries.GetById;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Queries.GetAll
{
	public class UserGetAllQuery : IRequest<ApplicationResult<UserListResponseModel>>
	{
		public class UserGetAllQueryHandler :
			IRequestHandler<UserGetAllQuery, ApplicationResult<UserListResponseModel>>
		{
			private readonly IUserManagerService _userManagerService;
			private readonly IMapper _mapper;

			public UserGetAllQueryHandler(
				IUserManagerService userManagerService, IMapper mapper)
			{
				_userManagerService = userManagerService;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<UserListResponseModel>> Handle(
				UserGetAllQuery request, CancellationToken cancellationToken)
			{
				var mappedUsers = await _mapper
					.ProjectTo<UserSimpleResponseModel>(_userManagerService.GetAllAsNoTracking().Response)
					.OrderBy(x => x.UserName)
					.ToAsyncEnumerable()
					.ToListAsync(cancellationToken);

				var mappedUserList = _mapper.Map<UserListResponseModel>(mappedUsers);

				return ApplicationResult<UserListResponseModel>.Success(mappedUserList);
			}
		}
	}
}
