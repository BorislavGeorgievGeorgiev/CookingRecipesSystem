namespace CookingRecipesSystem.Application.UnitTests.Common
{
	internal static class ClassExtensions
	{
		public static bool PublicPropertyExist<T>(this T obj, string propertyName, Type returnType)
			where T : class
		{
			return obj!.GetType().GetProperty(propertyName, returnType) != null;
		}

		public static bool PublicPropertyCanWrite<T>(this T obj, string propertyName)
			where T : class
		{
			return obj!.GetType().GetProperty(propertyName)!.CanWrite;
		}
	}
}
