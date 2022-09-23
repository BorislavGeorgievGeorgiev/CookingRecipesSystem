using CookingRecipesSystem.Application.Identity.Commands.ChangePasswordUser;
using CookingRecipesSystem.Application.Identity.Commands.LoginUser;
using CookingRecipesSystem.Application.Identity.Commands.RegisterUser;
using CookingRecipesSystem.Application.Identity.Queries.GetUsersAll;
using CookingRecipesSystem.Web.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Controllers
{
	[Authorize]
	public class IdentityController : BaseApiController
	{
		[AllowAnonymous]
		[HttpPost]
		[Route(nameof(Register))]
		public async Task<ActionResult> Register(
			[FromBody] RegisterUserCommand command)
			=> await Send(command);

		[AllowAnonymous]
		[HttpPost]
		[Route(nameof(Login))]
		public async Task<ActionResult<UserTokenResponseModel>> Login(
			[FromBody] LoginUserCommand command)
			=> await Send(command);

		[HttpPost]
		[Route(nameof(ChangePassword))]
		public async Task<ActionResult> ChangePassword(
			[FromBody] ChangePasswordUserCommand command)
			=> await Send(command);

		[HttpGet]
		[Route(nameof(GetAll))]
		public async Task<ActionResult<UsersListResponseModel>> GetAll(
			[FromQuery] GetUsersAllQuery query)
			=> await Send(query);
	}
}
