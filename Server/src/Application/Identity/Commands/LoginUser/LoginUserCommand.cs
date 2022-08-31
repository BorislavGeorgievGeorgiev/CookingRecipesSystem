using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.LoginUser
{
	public class LoginUserCommand
		: UserLoginRequestModel, IRequest<ApplicationResult<UserTokenResponseModel>>
	{
		public class LoginUserCommandHandler
			: IRequestHandler<LoginUserCommand, ApplicationResult<UserTokenResponseModel>>
		{
			private readonly IUserManagerService _userManagerService;
			private readonly IJwtService _jwtService;

			public LoginUserCommandHandler(IUserManagerService userManagerService,
				IJwtService jwtService)
			{
				this._userManagerService = userManagerService;
				this._jwtService = jwtService;
			}

			public async Task<ApplicationResult<UserTokenResponseModel>>
				Handle(LoginUserCommand request, CancellationToken cancellationToken)
			{

				var applicationResultUser = await this._userManagerService
					.FindByEmailAsync(request.Email);

				if (!applicationResultUser.Succeeded)
				{
					return ApplicationResult<UserTokenResponseModel>.Failure(
						ExceptionMessages.CredentialsInvalid);
				}

				var user = applicationResultUser.Response;

				var applicationResultPassword = await this._userManagerService
					.CheckPasswordAsync(user, request.Password);

				if (!applicationResultPassword.Succeeded)
				{
					return ApplicationResult<UserTokenResponseModel>.Failure(
						ExceptionMessages.CredentialsInvalid);
				}

				var token = await this._jwtService.GenerateToken(user.Id, request.Email);

				var response = new UserTokenResponseModel(token);

				return ApplicationResult<UserTokenResponseModel>.Success(response);
			}
		}
	}
}
