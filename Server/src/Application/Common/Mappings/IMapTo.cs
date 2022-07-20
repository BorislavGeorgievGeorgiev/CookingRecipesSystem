using AutoMapper;

namespace CookingRecipesSystem.Application.Common.Mappings
{
	public interface IMapTo<T>
	{
		void Mapping(Profile mapper) => mapper.CreateMap(this.GetType(), typeof(T));
	}
}
