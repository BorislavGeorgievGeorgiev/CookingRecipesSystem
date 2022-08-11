using System.Security.Claims;

using CookingRecipesSystem.Application.Common.Interfaces;

using Microsoft.AspNetCore.Http;

namespace CookingRecipesSystem.Web.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			GetUserId = httpContextAccessor
				.HttpContext?
				.User
				.FindFirstValue(nameof(ClaimTypes.NameIdentifier));

			IsAuthenticated = GetUserId != null;
		}

		public string? GetUserId { get; }
		public bool IsAuthenticated { get; }
	}
}
