using CookingRecipesSystem.Application.Identity.Commands.ChangePassword;
using CookingRecipesSystem.Application.Identity.Commands.Login;
using CookingRecipesSystem.Application.Identity.Commands.Register;
using CookingRecipesSystem.Application.Identity.Queries.GetAll;
using CookingRecipesSystem.Application.Identity.Queries.GetById;
using CookingRecipesSystem.Web.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Controllers
{
	public class IdentityController : BaseApiController
	{
		[AllowAnonymous]
		[HttpPost]
		[Route(nameof(Register))]
		public async Task<ActionResult> Register(
			[FromBody] RegisterCommand command)
			=> await Send(command);

		[AllowAnonymous]
		[HttpPost]
		[Route(nameof(Login))]
		public async Task<ActionResult<TokenResponseModel>> Login(
			[FromBody] LoginCommand command)
			=> await Send(command);

		[HttpPost]
		[Route(nameof(ChangePassword))]
		public async Task<ActionResult> ChangePassword(
			[FromBody] ChangePasswordCommand command)
			=> await Send(command);

		[HttpGet]
		[Route(nameof(GetAll))]
		public async Task<ActionResult<UserListResponseModel>> GetAll(
			[FromQuery] UserGetAllQuery query)
			=> await Send(query);

		[HttpGet]
		[Route(nameof(GetById) + "/" + Id)]
		public async Task<ActionResult<UserInfoResponseModel>> GetById(
			[FromRoute] UserGetByIdQuery query)
			=> await Send(query);
	}
}
