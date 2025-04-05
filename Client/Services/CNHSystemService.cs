using CNHephaestus.Objects.CNHService;
using Shared.CNH.Shared.Communication.Authentication;

namespace CNHephaestus.Services
{
 public class CNHSystemService
 {
  private ClientStatus _status = new();
  private ProxyService _proxyService;
  private AuthenticationService _authService;
  public string? SYSURL
  {
   get
   {
    return _status.SystemURL;
   }
   set
   {
    _status.SystemURL = value;
   }
  }
  public Session? CURRENTSESSION
  { get
   {
    return _status.currentSession;
   }
   set
   {
    _status.currentSession = value;
   }
  }

  public CNHSystemService(ProxyService ps)
  {
   _proxyService = ps;
   _authService = new AuthenticationService(this, ps);
  }

  public async Task<HttpResponseMessage?> checkForCNHapi(string url)
  {
   if (string.IsNullOrEmpty(url)) return null;

   if (url.Contains("/")) return null;

   url = "https://" + url;
   
   ProxyRequest request = new ProxyRequest($"{url}/CNH/APITest", ProxyService.Method.GET);
   int responseCode = await request.SendRequest();

   return request.Response;
  }

  public async Task<bool> initAuthenticate(string url, string username, string password)
  {

   return await _authService.InitAuthenticate(url, username, password);
  }

 }
}
