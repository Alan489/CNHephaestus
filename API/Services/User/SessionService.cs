using API.Services.Crypto;
using Shared.CNH.Shared.Communication.Authentication;

namespace API.Services.User
{
 public class SessionService
 {
  private CNHCryptoService _cryptoService;
  public SessionService(CNHCryptoService cryptoService)
  {
   _cryptoService = cryptoService;
  }
  public Session startSession(InitAuthentication ia)
  {
   Session response = new Session();
   response.start = DateTime.UtcNow;
   response.end = DateTime.UtcNow.AddDays(1);
   response.username = ia.username;
   response.sessionId = Guid.NewGuid();
   response.firstName = "Alan";
   response.lastName = "Decowski";
   response.sessionHash = ia.password;

   response.sessionHash = CNHCryptoService.SessionHash(response); 

   return response;
  }
 }
}
