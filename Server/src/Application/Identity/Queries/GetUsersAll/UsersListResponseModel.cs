using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Application.Identity.Queries.GetUserById;

namespace CookingRecipesSystem.Application.Identity.Queries.GetUsersAll
{
    public class UsersListResponseModel : IMapFrom<IEnumerable<UserSimpleResponseModel>>
	{
		public IEnumerable<UserSimpleResponseModel> Users { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<IEnumerable<UserSimpleResponseModel>, UsersListResponseModel>()
				.ForMember(d => d.Users, opt => opt.MapFrom(s => s));
		}
	}
}
