using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.ChangePassword
{
	public class ChangePasswordCommand :
		ChangePasswordRequestModel, IRequest<ApplicationResult>
	{
		public ChangePasswordCommand(
			string userId, string currentPassword, string newPassword)
			: base(userId, currentPassword, newPassword)
		{
		}

		public class ChangePasswordCommandHandler
			: IRequestHandler<ChangePasswordCommand, ApplicationResult>
		{
			private readonly IUserManagerService _userManagerService;

			public ChangePasswordCommandHandler(IUserManagerService userManagerService)
				=> _userManagerService = userManagerService;

			public async Task<ApplicationResult> Handle(
				ChangePasswordCommand request, CancellationToken cancellationToken)
			{
				var applicationResultUser = await _userManagerService
					.FindByIdAsync(request.UserId);

				if (!applicationResultUser.Succeeded)
				{
					return ApplicationResult.Failure(applicationResultUser.Errors);
				}

				var user = applicationResultUser.Response;

				return await _userManagerService.ChangePasswordAsync(
						user, request.CurrentPassword, request.NewPassword);
			}
		}
	}
}
