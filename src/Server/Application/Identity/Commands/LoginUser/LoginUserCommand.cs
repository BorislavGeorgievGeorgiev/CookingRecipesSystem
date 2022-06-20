using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.LoginUser
{
	public class LoginUserCommand
		: UserLoginRequestModel, IRequest<(ApplicationResult Result, UserTokenResponseModel Response)>
	{
		public LoginUserCommand(string email, string password)
			: base(email, password)
		{
		}

		public class LoginUserCommandHandler
			: IRequestHandler<LoginUserCommand,
				(ApplicationResult Result, UserTokenResponseModel Response)>
		{
			private readonly IIdentityService _identity;

			public LoginUserCommandHandler(IIdentityService identity)
				=> this._identity = identity;

			public async Task<(ApplicationResult Result, UserTokenResponseModel Response)>
				Handle(LoginUserCommand request, CancellationToken cancellationToken)
				=> await this._identity.Login(request);
		}
	}
}
