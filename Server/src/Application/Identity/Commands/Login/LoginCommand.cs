using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Domain.Common.Constants;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.Login
{
	public class LoginCommand
		: LoginRequestModel, IRequest<ApplicationResult<TokenResponseModel>>
	{
		public class LoginCommandHandler
			: IRequestHandler<LoginCommand, ApplicationResult<TokenResponseModel>>
		{
			private readonly IUserManagerService _userManagerService;
			private readonly IJwtService _jwtService;

			public LoginCommandHandler(IUserManagerService userManagerService,
				IJwtService jwtService)
			{
				_userManagerService = userManagerService;
				_jwtService = jwtService;
			}

			public async Task<ApplicationResult<TokenResponseModel>>
				Handle(LoginCommand request, CancellationToken cancellationToken)
			{

				var applicationResultUser = await _userManagerService
					.FindByEmailAsync(request.Email);

				if (!applicationResultUser.Succeeded)
				{
					return ApplicationResult<TokenResponseModel>.Failure(
						ExceptionMessages.CredentialsInvalid);
				}

				var user = applicationResultUser.Response;

				var applicationResultPassword = await _userManagerService
					.CheckPasswordAsync(user, request.Password);

				if (!applicationResultPassword.Succeeded)
				{
					return ApplicationResult<TokenResponseModel>.Failure(
						ExceptionMessages.CredentialsInvalid);
				}

				var token = await _jwtService.GenerateToken(user.Id, request.Email);

				var response = new TokenResponseModel(token);

				return ApplicationResult<TokenResponseModel>.Success(response);
			}
		}
	}
}
