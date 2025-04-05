using API.Services.User;
using Microsoft.AspNetCore.Mvc;
using Shared.CNH.Shared.Communication.Authentication;

namespace API.Controllers.Auhentication
{

 [ApiController]
 [Route("/CNH/Authenticate")]
 public class AuthenticationController : ControllerBase
 {
  private AuthenticationService _authenticationService { get; set; }
  public AuthenticationController(AuthenticationService ass)
  {
   _authenticationService = ass;
  }


  [HttpPost]
  public async Task<InitAuthenticationResponse> auth(InitAuthentication? ia)
  {
   if (ia == null) return new InitAuthenticationResponse();
   return await _authenticationService.Authenticate(ia);
  }
 }
}
