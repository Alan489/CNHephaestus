using CNHephaestus.Objects.CNHService;
using Shared.CNH.Shared.Communication.Authentication;
using System.Net.Http.Json;

namespace CNHephaestus.Services
{
 public class AuthenticationService
 {
  private ProxyService _proxyService;
  private CNHSystemService _systemService;
  public AuthenticationService(CNHSystemService sys, ProxyService proxy)
  {
   _systemService = sys;
   _proxyService = proxy;
  }

  public async Task<bool> InitAuthenticate(string url, string username, string password)
  {
   InitAuthentication auth = new InitAuthentication();
   auth.username = username;
   auth.password = password;

   ProxyRequest request = new ProxyRequest("https://" + url + "/CNH/Authenticate", ProxyService.Method.POST, null, auth);
   int hrm = await request.SendRequest();

   if (request.Response == null || request.Response.StatusCode != System.Net.HttpStatusCode.OK) return false;

   InitAuthenticationResponse? response = await request.Response.Content.ReadFromJsonAsync<InitAuthenticationResponse>();

   if (response == null || response.success == false) return false;

   if (response.session == null) return false;

   _systemService.CURRENTSESSION = response.session;
   _systemService.SYSURL = "https://" + url + "/CNH/";

   return true;
  }

 }
}
