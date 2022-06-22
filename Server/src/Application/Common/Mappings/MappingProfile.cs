using System.Reflection;

using AutoMapper;

namespace CookingRecipesSystem.Application.Common.Mappings
{
	public class MappingProfile : Profile
	{
		private const string _MappingFromMethodName = nameof(IMapFrom<int>.Mapping);
		private const string _MappingToMethodName = nameof(IMapTo<int>.Mapping);
		private readonly Type _iMapFromType = typeof(IMapFrom<>);
		private readonly Type _iMapToType = typeof(IMapTo<>);

		public MappingProfile()
			=> this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

		private void ApplyMappingsFromAssembly(Assembly assembly)
		{
			var typesMapFrom = assembly.GetExportedTypes()
				.Where(t => t
				.GetInterfaces()
				.Any(i => i.IsGenericType
				&& (i.GetGenericTypeDefinition() == this._iMapFromType
				|| i.GetGenericTypeDefinition() == this._iMapToType)))
				.ToList();

			foreach (var type in typesMapFrom)
			{
				var instance = Activator.CreateInstance(type);

				var methodInfo = this.GetCurrentMethodInfo(type);

				methodInfo?.Invoke(instance, new object[] { this });
			}
		}

		private MethodInfo? GetCurrentMethodInfo(Type type)
		{
			if (type.GetGenericTypeDefinition() == this._iMapFromType)
			{
				return type.GetMethod(_MappingFromMethodName)
					?? type.GetInterface(this._iMapFromType.Name)!.GetMethod(_MappingFromMethodName);
			}
			else
			{
				return type.GetMethod(_MappingToMethodName)
					?? type.GetInterface(this._iMapToType.Name)!.GetMethod(_MappingToMethodName);
			}
		}
	}
}
