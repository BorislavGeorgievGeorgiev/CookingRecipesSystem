namespace CookingRecipesSystem.Domain.Common.Constants
{
	public static class AppConstants
	{
		public const string DefaultConnection = "DefaultConnection";

		public const string RoleNameAdministrator = "Administrator ";

		//======================================
		public const bool PasswordRequireNonAlphanumericValue = false;
		public const bool PasswordRequireDigitValue = false;
		public const bool PasswordRequireUppercaseValue = false;
		public const int PasswordRequiredUniqueCharsValue = 0;
		public const int PasswordMinLength = 3;
		public const int PasswordMaxLength = 50;
		public const int UserNameMinLength = 1;
		public const int UserNameMaxLength = 50;

		//======================================
		public const string ColumnTypeImage = "image";
	}
}
