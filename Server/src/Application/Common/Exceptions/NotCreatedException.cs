namespace CookingRecipesSystem.Application.Common.Exceptions
{
	public class NotCreatedException : Exception
	{
		public NotCreatedException(string entityName)
				: base($"Entity '{entityName}' was not created.")
		{
		}
	}
}
