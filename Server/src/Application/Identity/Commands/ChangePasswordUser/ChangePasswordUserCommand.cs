using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.ChangePasswordUser
{
	public class ChangePasswordUserCommand : ChangePasswordRequestModel, IRequest<ApplicationResult>
	{
		public ChangePasswordUserCommand(
			string userId, string currentPassword, string newPassword)
			: base(userId, currentPassword, newPassword)
		{
		}

		public class ChangePasswordUserCommandHandler
			: IRequestHandler<ChangePasswordUserCommand, ApplicationResult>
		{
			private readonly IIdentityService _identity;

			public ChangePasswordUserCommandHandler(IIdentityService identity)
				=> this._identity = identity;

			public async Task<ApplicationResult> Handle(
				ChangePasswordUserCommand request, CancellationToken cancellationToken)
				=> await this._identity.ChangePassword(request);
		}
	}
}
