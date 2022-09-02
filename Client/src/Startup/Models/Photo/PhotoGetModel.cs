namespace CookingRecipesSystem.Startup.Models.Photo
{
  public class PhotoGetModel
  {
    private string _mainPhoto;
    private string _cardPhoto;
    private string _thumbnail;

    public string MainPhoto
    {
      get { return _mainPhoto; }
      set { _mainPhoto = GetFormatedValue(value); }
    }

    public string CardPhoto
    {
      get { return _cardPhoto; }
      set { _cardPhoto = GetFormatedValue(value); }
    }

    public string Thumbnail
    {
      get { return _thumbnail; }
      set { _thumbnail = GetFormatedValue(value); }
    }

    private static string GetFormatedValue(string value)
    {
      return string.Format("data:image/jpeg;base64,{0}", value);
    }
  }
}
