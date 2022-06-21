using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.LoginUser
{
	public class LoginUserCommand
		: UserLoginRequestModel, IRequest<ApplicationResult<UserTokenResponseModel>>
	{
		public LoginUserCommand(string email, string password)
			: base(email, password)
		{
		}

		public class LoginUserCommandHandler
			: IRequestHandler<LoginUserCommand, ApplicationResult<UserTokenResponseModel>>
		{
			private readonly IIdentityService _identity;

			public LoginUserCommandHandler(IIdentityService identity)
				=> this._identity = identity;

			public async Task<ApplicationResult<UserTokenResponseModel>>
				Handle(LoginUserCommand request, CancellationToken cancellationToken)
				=> await this._identity.Login(request);
		}
	}
}
