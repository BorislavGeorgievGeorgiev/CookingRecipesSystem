namespace CookingRecipesSystem.Startup.Constants
{
	public static class PageRoutes
	{
		public const string ReturnUrl = "?returnUrl=";
		public const string Id = "{id:int}";
		//============================================
		public const string Home = "/";
		//============================================
		public const string Register = "/authentication/register/";
		public const string Login = "/authentication/login/";
		public const string GetAllUsers = "/authentication/getall/";
		public const string Profile = "/authentication/profile/";
		//============================================
		public const string IngredientCreate = "/ingredient/create/";
		public const string IngredientGetById = "/ingredient/getbyid/";
	}
}
