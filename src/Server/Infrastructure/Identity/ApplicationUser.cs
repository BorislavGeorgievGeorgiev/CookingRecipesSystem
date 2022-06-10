using CookingRecipesSystem.Application.Common.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace CookingRecipesSystem.Infrastructure.Identity
{
	internal class ApplicationUser : IdentityUser, IApplicationUser
	{
	}
}
