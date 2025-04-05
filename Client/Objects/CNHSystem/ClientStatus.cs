using Shared.CNH.Shared.Communication.Authentication;

namespace CNHephaestus.Objects.CNHService
{
 public class ClientStatus
 {
  public string? SystemURL { get; set; }

  private Session? _SESSION { get; set; }
  public Session? currentSession 
  { get
   {
    if (_SESSION == null) return null;
    if (DateTime.UtcNow >= _SESSION.end) return null;
    return _SESSION;
   }
   set
   {
    _SESSION = value;
   }
  }


 }
}
