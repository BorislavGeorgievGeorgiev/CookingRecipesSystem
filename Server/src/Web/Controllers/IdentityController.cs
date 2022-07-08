using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Identity.Commands.ChangePasswordUser;
using CookingRecipesSystem.Application.Identity.Commands.CreateTestEntity;
using CookingRecipesSystem.Application.Identity.Commands.LoginUser;
using CookingRecipesSystem.Application.Identity.Commands.RegisterUser;
using CookingRecipesSystem.Application.Identity.Queries.AllTestEntity;
using CookingRecipesSystem.Web.Common;
using CookingRecipesSystem.Web.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Controllers
{
	public class IdentityController : BaseApiController
	{
		[HttpPost]
		[Route(nameof(Register))]
		public async Task<ActionResult> Register(
			RegisterUserCommand command)
			=> await this.Mediator.Send(command).ToActionResult();

		[HttpPost]
		[Route(nameof(Login))]
		public async Task<ActionResult<UserTokenResponseModel>> Login(
			LoginUserCommand command)
			=> await this.Mediator.Send(command).ToActionResult();

		[Authorize]
		[HttpPost]
		[Route(nameof(ChangePassword))]
		public async Task<ActionResult> ChangePassword(
			ChangePasswordUserCommand command)
			=> await this.Mediator.Send(command).ToActionResult();

		[HttpGet]
		[Route(nameof(GetAllTestEntities))]
		public async Task<ActionResult<TestEntityListResponseModel>> GetAllTestEntities(
			[FromQuery] TestEntitiesQuery query)
			=> await this.Mediator.Send(query).ToActionResult();

		[Authorize]
		[HttpPost]
		[Route(nameof(CreateTestEntity))]
		public async Task<ActionResult> CreateTestEntity(
			CreateTestEntityCommand command)
			=> await this.Mediator.Send(command).ToActionResult();
	}
}
