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

		private static PropertyInfo? GetCurrentProperty<T>(T obj, string propertyName, Type returnType)
		{
			return obj!.GetType().GetProperty(propertyName, returnType);
		}
	}
}
