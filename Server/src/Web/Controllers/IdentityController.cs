using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Identity.Commands.ChangePasswordUser;
using CookingRecipesSystem.Application.Identity.Commands.LoginUser;
using CookingRecipesSystem.Application.Identity.Commands.RegisterUser;
using CookingRecipesSystem.Application.Identity.Queries.GetUsersAll;
using CookingRecipesSystem.Web.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Controllers
{
	public class IdentityController : BaseApiController
	{
		[HttpPost]
		[Route(nameof(Register))]
		public async Task<ActionResult<ApplicationResult>> Register(
			[FromBody] RegisterUserCommand command)
			=> await Send(command);

		[HttpPost]
		[Route(nameof(Login))]
		public async Task<ActionResult<ApplicationResult<UserTokenResponseModel>>> Login(
			[FromBody] LoginUserCommand command)
			=> await Send(command);

		[Authorize]
		[HttpPost]
		[Route(nameof(ChangePassword))]
		public async Task<ActionResult<ApplicationResult>> ChangePassword(
			[FromBody] ChangePasswordUserCommand command)
			=> await Send(command);


		[HttpGet]
		[Route(nameof(GetAll))]
		public async Task<ActionResult<ApplicationResult<UsersListResponseModel>>> GetAll(
			[FromQuery] GetUsersAllQuery query)
			=> await Send(query);
	}
}
