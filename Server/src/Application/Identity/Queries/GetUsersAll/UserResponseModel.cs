using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Mappings;

namespace CookingRecipesSystem.Application.Identity.Queries.GetUsersAll
{
	public class UserResponseModel : IMapFrom<IApplicationUser>
	{
		public string UserName { get; set; }
	}
}
