using Microsoft.Extensions.Logging;

namespace CNHephaestus.Pages.Components.Modals
{
 public class ModalClass
 {
  public string type { get; set; }
  public string title { get; set; }
  public string description { get; set; }
  public event EventHandler<EventArgs> EventOk;
  public event EventHandler<EventArgs> EventCancel;

  public void FireOk()
  {
   EventOk?.Invoke(this, EventArgs.Empty);
  }

  public void FireCancel()
  {
   EventCancel?.Invoke(this, EventArgs.Empty);
  }

  public void ClearDelegates()
  {
   foreach (Delegate d in EventOk.GetInvocationList())
   {
    EventOk -= (EventHandler<EventArgs>)d;
   }
   foreach (Delegate d in EventCancel.GetInvocationList())
   {
    EventCancel -= (EventHandler<EventArgs>)d;
   }
  }

 }
}
