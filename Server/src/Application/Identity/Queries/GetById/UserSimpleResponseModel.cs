using CookingRecipesSystem.Application.Common.Interfaces;
using CookingRecipesSystem.Application.Common.Mappings;

namespace CookingRecipesSystem.Application.Identity.Queries.GetById
{
	public class UserSimpleResponseModel : IMapFrom<IApplicationUser>
	{
		public string UserName { get; set; }
	}
}
