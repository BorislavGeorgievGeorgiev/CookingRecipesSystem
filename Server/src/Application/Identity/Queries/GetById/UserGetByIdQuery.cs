using AutoMapper;

using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Queries.GetById
{
	public class UserGetByIdQuery : IRequest<ApplicationResult<UserInfoResponseModel>>
	{
		public string Id { get; set; }

		public class UserGetByIdQueryHandler :
			IRequestHandler<UserGetByIdQuery, ApplicationResult<UserInfoResponseModel>>
		{
			private readonly IUserManagerService _userManagerService;
			private readonly IMapper _mapper;

			public UserGetByIdQueryHandler(
				IUserManagerService userManagerService, IMapper mapper)
			{
				_userManagerService = userManagerService;
				_mapper = mapper;
			}

			public async Task<ApplicationResult<UserInfoResponseModel>> Handle(
				UserGetByIdQuery request, CancellationToken cancellationToken)
			{
				var userResult = await _userManagerService.FindByIdAsync(request.Id);

				if (userResult.Succeeded == false)
				{
					return ApplicationResult<UserInfoResponseModel>.Failure(
						ExceptionMessages.UserInvalid);
				}

				var mappedUser = _mapper.Map<UserInfoResponseModel>(userResult.Response);

				return ApplicationResult<UserInfoResponseModel>.Success(mappedUser);
			}
		}
	}
}
