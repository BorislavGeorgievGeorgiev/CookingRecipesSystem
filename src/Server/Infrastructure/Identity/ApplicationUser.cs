using CookingRecipesSystem.Application.Common.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace CookingRecipesSystem.Infrastructure.Identity
{
	public class ApplicationUser : IdentityUser, IApplicationUser
	{
	}
}
