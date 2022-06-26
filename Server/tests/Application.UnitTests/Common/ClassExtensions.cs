using System.Reflection;

namespace CookingRecipesSystem.Application.UnitTests.Common
{
	internal static class ClassExtensions
	{
		public static bool PublicPropertyExist<T>(this T obj, string propertyName, Type returnType)
			where T : class
		{
			return GetCurrentProperty(obj, propertyName, returnType) != null;
		}

		public static bool PublicPropertyCanWrite<T>(this T obj, string propertyName, Type returnType)
			where T : class
		{
			return GetCurrentProperty(obj, propertyName, returnType)!.CanWrite;
		}

		public static bool PublicPropertyIsStatic<T>(this T obj, string propertyName, Type returnType)
			where T : class
		{
			return GetCurrentProperty(obj, propertyName, returnType)!.GetGetMethod()!.IsStatic;
		}

		public static bool PublicMethodExist<T>(this T obj,
			string propertyName, Type returnType, Type[] parametersTypes)
			where T : class
		{
			var method = GetCurrentMethod(obj, propertyName, parametersTypes);

			return (method != null) && (method.ReturnType == returnType);
		}

		public static bool PublicMethodIsStatic<T>(this T obj,
			string propertyName, Type[] parametersTypes)
			where T : class
		{
			return GetCurrentMethod(obj, propertyName, parametersTypes)!.IsStatic;
		}

		private static PropertyInfo? GetCurrentProperty<T>(T obj, string propertyName, Type returnType)
		{
			return obj!.GetType().GetProperty(propertyName, returnType);
		}

		private static MethodInfo? GetCurrentMethod<T>(T obj, string methodName, Type[] parametersTypes)
		{
			return obj!.GetType().GetMethod(methodName, parametersTypes);
		}
	}
}
