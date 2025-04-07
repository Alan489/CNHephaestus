namespace CNHephaestus.Pages.Components.Windows
{
 public class Window
 {
  public string Title { get; set; }
  public string Type { get; set; }
  public List<string>? Data { get; set; }
  public List<string> Memory { get; private set; } = new List<string>();

  public Window(string type, string title, List<string>? data = null)
  {
   Title = title;
   Type = type;
   Data = data;
  }
 }
}
