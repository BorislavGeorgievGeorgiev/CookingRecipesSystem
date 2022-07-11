
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Web.Extensions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CookingRecipesSystem.Web.Common
{
	[ApiController]
	[Route("api/[controller]")]
	public abstract class BaseApiController : ControllerBase
	{
		private IMediator? _mediator;

		protected const string Id = "{id}";

		private IMediator Mediator
				=> this._mediator ??= this.HttpContext
						.RequestServices
						.GetService<IMediator>()!;

		protected async Task<ActionResult> Send(IRequest<ApplicationResult> request)
		{
			return await this.Mediator.Send(request).ToActionResult();
		}

		protected async Task<ActionResult<TModel>> Send<TModel>(
			IRequest<ApplicationResult<TModel>> request)
			where TModel : class
		{
			return await this.Mediator.Send(request).ToActionResult();
		}
	}
}
