using System.Data;

namespace API.Services.DataAccess.Interface
{
 public interface IDBAccess
 {
  public DataTable? Get(string query);
  public bool Insert(string tableName, DataTable dt);

 }
}
