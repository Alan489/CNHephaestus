using API.Services.DataAccess.Interface;
using System.Data;

namespace API.Services.User
{
 public class UserService
 {
  private IDBAccess _db;
  public UserService(IDBAccess dba)
  {
   _db = dba;
  }

  public DataTable? GetUser(string username, string password)
  {
   return _db.Get($"SELECT * FROM CNH_Users WHERE LOWER(Username) = '{username.ToLower()}' AND Passhash = '{password}'");
  }

 }
}
