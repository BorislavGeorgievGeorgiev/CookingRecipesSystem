using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Interfaces.Factories;
using CookingRecipesSystem.Application.Common.Models;

using MediatR;

namespace CookingRecipesSystem.Application.Identity.Commands.RegisterUser
{
	public class RegisterUserCommand : UserRegisterRequestModel, IRequest<ApplicationResult>
	{
		public class RegisterUserCommandHandler
			: IRequestHandler<RegisterUserCommand, ApplicationResult>
		{
			private readonly IApplicationUserFactory _applicationUserFactory;
			private readonly IUserManagerService _userManagerService;

			public RegisterUserCommandHandler(
				IUserManagerService userManagerService,
				IApplicationUserFactory applicationUserFactory)
			{
				_applicationUserFactory = applicationUserFactory;
				_userManagerService = userManagerService;
			}

			public async Task<ApplicationResult> Handle(
				RegisterUserCommand request, CancellationToken cancellationToken)
			{
				var user = _applicationUserFactory
					.With(request.UserName, request.Email)
					.Create();

				return await _userManagerService.CreateAsync(user, request.Password);
			}
		}
	}
}
