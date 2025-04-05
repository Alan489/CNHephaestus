using API.Services.Crypto;
using Shared.CNH.Shared.Communication.Authentication;
using System.Data;

namespace API.Services.User
{
 public class SessionService
 {
  private CNHCryptoService _cryptoService;
  public SessionService(CNHCryptoService cryptoService)
  {
   _cryptoService = cryptoService;
  }
  public Session startSession(DataRow dr)
  {
   Session response = new Session();
   response.start = DateTime.UtcNow;
   response.end = DateTime.UtcNow.AddDays(1);
   response.username = dr["Username"].ToString();
   response.sessionId = Guid.NewGuid();
   response.firstName = dr["Firstname"].ToString(); ;
   response.lastName = dr["Lastname"].ToString();
   response.sessionHash = dr["Passhash"].ToString();
   response.userGUID = Guid.Parse(dr["Guid"].ToString());

   response.sessionHash = CNHCryptoService.SessionHash(response);

   return response;
  }
 }
}
