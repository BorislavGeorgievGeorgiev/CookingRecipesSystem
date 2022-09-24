namespace CookingRecipesSystem.Domain.Common.Constants
{
	public static class ExceptionMessages
	{
		public const string CreatorIdNotNull = "The creator user ID cannot be null.";
		public const string ModifierIdNotNull = "The modifier user ID cannot be null.";
		public const string ModifiedOnNotNull = "The modified date cannot be null.";

		public const string ModelValidationFailures = "One or more validation failures have occurred.";

		// UserManagerService
		public const string UserInvalid = "The user not exist.";
		public const string PasswordInvalid = "The password is not valid.";

		// LoginUserCommand
		public const string CredentialsInvalid = "Invalid credentials.";

		// CreateIngredientCommand
		public const string IngredientExist = "This ingredient exist.";

		// GetIngredientQuery
		public const string IngredientInvalid = "There is no such ingredient.";

		// DeleteIngredientCommand
		public const string IngredientNotDeleted = "Cannot delete this ingredient.";
		public const string IngredientPhotoNotDeleted = "Cannot delete this ingredient photos.";
	}
}
