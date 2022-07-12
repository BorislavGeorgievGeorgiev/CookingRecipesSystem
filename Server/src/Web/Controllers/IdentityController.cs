using CookingRecipesSystem.Application.Identity.Commands.ChangePasswordUser;
using CookingRecipesSystem.Application.Identity.Commands.CreateTestEntity;
using CookingRecipesSystem.Application.Identity.Commands.LoginUser;
using CookingRecipesSystem.Application.Identity.Commands.RegisterUser;
using CookingRecipesSystem.Application.Identity.Queries.AllTestEntity;
using CookingRecipesSystem.Web.Common;

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
			=> await this.Send(command);

		[HttpPost]
		[Route(nameof(Login))]
		public async Task<ActionResult<UserTokenResponseModel>> Login(
			LoginUserCommand command)
			=> await this.Send(command);

		[Authorize]
		[HttpPost]
		[Route(nameof(ChangePassword))]
		public async Task<ActionResult> ChangePassword(
			ChangePasswordUserCommand command)
			=> await this.Send(command);

		[HttpGet]
		[Route(nameof(GetAllTestEntities))]
		public async Task<ActionResult<TestEntityListResponseModel>> GetAllTestEntities(
			[FromQuery] TestEntitiesQuery query)
			=> await this.Send(query);

		[Authorize]
		[HttpPost]
		[Route(nameof(CreateTestEntity))]
		public async Task<ActionResult> CreateTestEntity(
			CreateTestEntityCommand command)
		{
			return await this.Send(command);
		}
	}
}
