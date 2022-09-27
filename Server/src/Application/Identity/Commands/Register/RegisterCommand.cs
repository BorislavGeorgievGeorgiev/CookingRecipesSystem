using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Interfaces.Factories;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.Register
{
	public class RegisterCommand : RegisterRequestModel, IRequest<ApplicationResult>
	{
		public class RegisterCommandHandler
			: IRequestHandler<RegisterCommand, ApplicationResult>
		{
			private readonly IApplicationUserFactory _applicationUserFactory;
			private readonly IUserManagerService _userManagerService;

			public RegisterCommandHandler(
				IUserManagerService userManagerService,
				IApplicationUserFactory applicationUserFactory)
			{
				_applicationUserFactory = applicationUserFactory;
				_userManagerService = userManagerService;
			}

			public async Task<ApplicationResult> Handle(
				RegisterCommand request, CancellationToken cancellationToken)
			{
				var user = _applicationUserFactory
					.With(request.UserName, request.Email)
					.Create();

				return await _userManagerService.CreateAsync(user, request.Password);
			}
		}
	}
}
