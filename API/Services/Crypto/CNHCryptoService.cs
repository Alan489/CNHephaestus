using Shared.CNH.Shared.Communication.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace API.Services.Crypto
{
 public class CNHCryptoService
 {

 public static byte[] GetHash(string inputString)
  {
   using (HashAlgorithm algorithm = SHA256.Create())
    return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
  }

  public static string GetHashString(string inputString)
  {
   StringBuilder sb = new StringBuilder();
   foreach (byte b in GetHash(inputString))
    sb.Append(b.ToString("X2"));

   return sb.ToString();
  }

  public static string SessionHash(Session session)
  {
   string sr = JsonSerializer.Serialize<Session>(session);
   return GetHashString(sr);
  }

  public static string hashPassword(string username, string password)
  {
   string salt = "KkSJiciSOp02=S!kis";
   string pepper = "pepper!";
   string toHash = $"{salt}{password}{pepper}{username.ToLower()}";
   return GetHashString(toHash);
  }

 }
}
