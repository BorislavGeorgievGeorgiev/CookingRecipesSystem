namespace CookingRecipesSystem.Domain.Common.Constants
{
	public static class ExceptionMessages
	{
		public const string CreatorIdNotNull = "The creator user ID cannot be null.";
		public const string ModifierIdNotNull = "The modifier user ID cannot be null.";
		public const string ModifiedOnNotNull = "The modified date cannot be null.";

		public const string ModelValidationFailures = "One or more validation failures have occurred.";

		// UserManagerService GetUserName()
		public const string InvalidUser = "The user not exist.";
		// UserManagerService CheckPassword()
		public const string InvalidPassword = "The password is not valid.";

		// LoginUserCommand
		public const string InvalidCredentials = "Invalid credentials.";

		// GetIngredientQuery
		public const string InvalidIngredient = "There is no such ingredient.";
	}
}
