using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.ChangePasswordUser
{
	public class ChangePasswordUserCommand :
		ChangePasswordRequestModel, IRequest<ApplicationResult>
	{
		public ChangePasswordUserCommand(
			string userId, string currentPassword, string newPassword)
			: base(userId, currentPassword, newPassword)
		{
		}

		public class ChangePasswordUserCommandHandler
			: IRequestHandler<ChangePasswordUserCommand, ApplicationResult>
		{
			private readonly IUserManagerService _userManagerService;

			public ChangePasswordUserCommandHandler(IUserManagerService userManagerService)
				=> this._userManagerService = userManagerService;

			public async Task<ApplicationResult> Handle(
				ChangePasswordUserCommand request, CancellationToken cancellationToken)
			{
				var applicationResultUser = await this._userManagerService
					.FindByIdAsync(request.UserId);

				if (!applicationResultUser.Succeeded)
				{
					return ApplicationResult.Failure(applicationResultUser.Errors);
				}

				var user = applicationResultUser.Response;

				return await this._userManagerService.ChangePasswordAsync(
						user, request.CurrentPassword, request.NewPassword);
			}
		}
	}
}
