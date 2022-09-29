namespace CookingRecipesSystem.Startup.Constants
{
  public static class ErrorMessages
  {
    public const string InvalidStringLength = "The {0} must be at least {2} and at max {1} characters long.";
    public const string InvalidPasswordConfirm = "The password and confirmation password do not match.";
    public const string InvalidEmail = "The {0} field is not a valid e-mail address.";
    //==================================================
    public const string ServerNoFound = "Cannot connect to the server.";
    public const string NotLogedIn = "You are not logged in.";
    //==================================================
    public const string ResultInvalid = "Cannot return result.";
    public const string IngredientInvalid = "No Ingredient";
  }
}
