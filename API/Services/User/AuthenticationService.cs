using Shared.CNH.Shared.Communication.Authentication;

namespace API.Services.User
{
 public class AuthenticationService
 {
  private SessionService _sessionService;
  public AuthenticationService(SessionService sessionService) 
  { 
   _sessionService = sessionService;
  }


  public async Task<InitAuthenticationResponse> Authenticate(InitAuthentication ia)
  {
   InitAuthenticationResponse response = new InitAuthenticationResponse();
   response.success = false;

   if (ia.username == "adecowski" && ia.password == "123456")
   {
    Session sess = _sessionService.startSession(ia);
    response.success = true;
    response.session = sess;
   }

   return response;
  }


 }
}
