using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;

namespace CookingRecipesSystem.Application.Identity.Queries.GetUsersAll
{
	public class UsersListResponseModel : IMapFrom<IEnumerable<UserResponseModel>>
	{
		public IEnumerable<UserResponseModel> Users { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<IEnumerable<UserResponseModel>, UsersListResponseModel>()
				.ForMember(d => d.Users, opt => opt.MapFrom(s => s));
		}
	}
}
