namespace CookingRecipesSystem.Startup.Shared
{
	public static class AppConstants
	{
		public const string AuthTokenName = "authToken";
		public const string BearerName = "bearer";
		//============================================
		public const string RoleNameAdministrator = "Administrator";
		public const string RoleNameModerator = "Moderator";
		//============================================
		public const string EmailRegEx = "^[A-Za-z0-9]+[\\._A-Za-z0-9-]+@([A-Za-z0-9]+[-\\.]?[A-Za-z0-9]+)+(\\.[A-Za-z0-9]+[-\\.]?[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";
		//============================================
		public const int PasswordMinLength = 3;
		public const int PasswordMaxLength = 50;
		public const int UserNameMinLength = 1;
		public const int UserNameMaxLength = 50;
		//============================================
		public const string UserNameDisplay = "User Name";
		public const string PasswordConfirmDisplay = "Confirm Password";
	}
}
