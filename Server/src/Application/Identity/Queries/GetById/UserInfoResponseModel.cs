using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Mappings;

namespace CookingRecipesSystem.Application.Identity.Queries.GetById
{
	public class UserInfoResponseModel : IMapFrom<IApplicationUser>
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }
	}
}
