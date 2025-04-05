using API.Services.DataAccess.Interface;
using System.Data;
using Microsoft.Data.Sqlite;

namespace API.Services.DataAccess
{
 public class SQLLiteAccess : IDBAccess
 {

  private string _sqlLiteFile;
  

  public SQLLiteAccess(IConfiguration config) 
  {
   string? t = config.GetSection("DataAccess").GetValue<string>("File");
   if (string.IsNullOrEmpty(t) || !t.EndsWith(".db")) 
   {
    throw new DataException("Invalid File");
   }
   _sqlLiteFile = t;
  }

  public DataTable? Get(string query)
  {
   try
   {
    using (var connection = new SqliteConnection($"DataSource={_sqlLiteFile}"))
    {
     connection.Open();
     var command = connection.CreateCommand();
     command.CommandText = query;
     using (var reader = command.ExecuteReader())
     {
      DataTable dt = new DataTable();
      dt.Load(reader);
      return dt;
     }
    }
   } catch(Exception e)
   {
    Console.WriteLine(e.Message);
    Console.WriteLine(e.StackTrace);
    return null;
   }
  }

  public bool Insert(string tableName, DataTable dt)
  {
   throw new NotImplementedException();
  }
 }
}
