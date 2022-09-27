using AutoMapper;

using CookingRecipesSystem.Application.Common.Mappings;
using CookingRecipesSystem.Application.Identity.Queries.GetById;

namespace CookingRecipesSystem.Application.Identity.Queries.GetAll
{
	public class UserListResponseModel : IMapFrom<IEnumerable<UserSimpleResponseModel>>
	{
		public IEnumerable<UserSimpleResponseModel> Users { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<IEnumerable<UserSimpleResponseModel>, UserListResponseModel>()
				.ForMember(d => d.Users, opt => opt.MapFrom(s => s));
		}
	}
}
