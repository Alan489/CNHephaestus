using API.Services.Crypto;
using Shared.CNH.Shared.Communication.Authentication;
using System.Data;

namespace API.Services.User
{
 public class AuthenticationService
 {
  private SessionService _sessionService;
  private UserService _userService;
  public AuthenticationService(SessionService sessionService, UserService us) 
  { 
   _sessionService = sessionService;
   _userService = us;
  }


  public async Task<InitAuthenticationResponse> Authenticate(InitAuthentication ia)
  {
   InitAuthenticationResponse response = new InitAuthenticationResponse();
   response.success = false;

   ia.password = CNHCryptoService.hashPassword(ia.username, ia.password);

   DataTable? user = _userService.GetUser(ia.username, ia.password);

   if (user == null || user.Rows.Count == 0) return response;
   Session sess = _sessionService.startSession(user.Rows[0]);
   response.success = true;
   response.session = sess;

   return response;
  }


 }
}
