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
			=> ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

		private void ApplyMappingsFromAssembly(Assembly assembly)
		{
			var typesMapFromOrTo = assembly.GetExportedTypes()
				.Where(t => t
				.GetInterfaces()
				.Any(i => i.IsGenericType
				&& (i.GetGenericTypeDefinition() == _iMapFromType
				|| i.GetGenericTypeDefinition() == _iMapToType)))
				.ToList();

			foreach (var type in typesMapFromOrTo)
			{
				var iMapNames = GetIMapImplementedInterfacesNames(type);

				foreach (var name in iMapNames)
				{
					var instance = Activator.CreateInstance(type);

					var methodInfo = GetMethodInfo(name, type);

					methodInfo?.Invoke(instance, new object[] { this });
				}
			}
		}

		private MethodInfo? GetMethodInfo(string name, Type type)
		{
			MethodInfo? methodInfo = null;

			if (name == _iMapFromType.Name)
			{
				methodInfo = type.GetMethod(_MappingFromMethodName)
				 ?? type.GetInterface(_iMapFromType.Name)!.GetMethod(_MappingFromMethodName);
			}

			if (name == _iMapToType.Name)
			{
				methodInfo = type.GetMethod(_MappingToMethodName)
					?? type.GetInterface(_iMapToType.Name)!.GetMethod(_MappingToMethodName);
			}

			return methodInfo;
		}

		private IEnumerable<string> GetIMapImplementedInterfacesNames(Type type)
		{
			var implementedInterfaces = type.GetTypeInfo().ImplementedInterfaces;
			var listNames = new List<string>();

			foreach (var implementedInterface in implementedInterfaces)
			{
				if (implementedInterface.Name == _iMapFromType.Name)
				{
					listNames.Add(implementedInterface.Name);
				}

				if (implementedInterface.Name == _iMapToType.Name)
				{
					listNames.Add(implementedInterface.Name);
				}
			}

			return listNames;
		}
	}
}
