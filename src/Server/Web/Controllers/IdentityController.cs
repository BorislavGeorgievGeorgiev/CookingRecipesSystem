using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Application.Identity.Commands.LoginUser;
using CookingRecipesSystem.Web.Common;
using CookingRecipesSystem.Web.Extensions;

using Microsoft.AspNetCore.Mvc;
namespace CookingRecipesSystem.Web.Controllers
{
	public class IdentityController : BaseApiController
	{
		[HttpPost]
		[Route(nameof(Login))]
		public async Task<ActionResult<UserTokenResponseModel>> Login(
			LoginUserCommand command)
			=> await this.Mediator.Send(command).ToActionResult();
	}
}
