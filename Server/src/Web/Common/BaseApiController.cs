
using CookingRecipesSystem.Application.Common.Models;
using CookingRecipesSystem.Web.Common.Extensions;

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

		protected const string Id = "{Id}";

		private IMediator Mediator
				=> _mediator ??= HttpContext
						.RequestServices
						.GetService<IMediator>()!;

		protected async Task<ActionResult<ApplicationResult>> Send(IRequest<ApplicationResult> request)
		{
			return await Mediator.Send(request).ToActionResult();
		}

		protected async Task<ActionResult<ApplicationResult<TModel>>> Send<TModel>(
			IRequest<ApplicationResult<TModel>> request)
			where TModel : class
		{
			return await Mediator.Send(request).ToActionResult();
		}
	}
}
