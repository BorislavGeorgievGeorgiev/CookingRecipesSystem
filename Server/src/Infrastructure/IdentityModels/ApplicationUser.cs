using CookingRecipesSystem.Application.Common.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace CookingRecipesSystem.Infrastructure.IdentityModels
{
	public class ApplicationUser : IdentityUser, IApplicationUser
	{
	}
}
