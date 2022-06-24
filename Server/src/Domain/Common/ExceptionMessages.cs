namespace CookingRecipesSystem.Domain.Common
{
	public static class ExceptionMessages
	{
		public const string CreatorIdNotNull = "The creator user ID cannot be null.";
		public const string ModifierIdNotNull = "The modifier user ID cannot be null.";
		public const string ModifiedOnNotNull = "The modified date cannot be null.";

		public const string ModelValidationFailures = "One or more validation failures have occurred.";

		public const string NoUser = "There is no such user.";
		public const string NoValidPassowrd = "The passowrd is not valid.";

		public const string InvalidCredentials = "Invalid credentials.";
	}
}
