using CookingRecipesSystem.Infrastructure.Identity;
using CookingRecipesSystem.Infrastructure.Services;
using CookingRecipesSystem.Web.Common;

using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesSystem.Web.Controllers
{
	public class TokenController : ApiController
	{
		private IJwtService _jwtService;

		public TokenController(IJwtService jwtService)
		{
			this._jwtService = jwtService;
		}
		[HttpGet]
		public string GetRandomToken()
		{
			var token = this._jwtService.GenerateToken(new ApplicationUser
			{
				Id = Guid.NewGuid().ToString(),
				Email = "testc@test.com"
			});

			return token.Result;
		}
	}
}
