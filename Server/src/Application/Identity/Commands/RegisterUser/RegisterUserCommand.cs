using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.RegisterUser
{
	public class RegisterUserCommand : UserRegisterRequestModel, IRequest<ApplicationResult>
	{
		public RegisterUserCommand(string userName, string email, string password)
					: base(userName, email, password)
		{
		}

		public class RegisterUserCommandHandler
			: IRequestHandler<RegisterUserCommand, ApplicationResult>
		{
			private readonly IIdentityService _identity;

			public RegisterUserCommandHandler(IIdentityService identity)
				=> this._identity = identity;

			public async Task<ApplicationResult> Handle(
				RegisterUserCommand request, CancellationToken cancellationToken)
				=> await this._identity.Register(request);
		}
	}
}
