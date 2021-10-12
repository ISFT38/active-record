using Npgsql;

namespace active_record
{
  public class Database {

    private static string connString = "Host=localhost;Username=arecord_user;Password=arecord_pass;Database=active_record";
    private static NpgsqlConnection connection;

    private Database() {}
    
    public static NpgsqlConnection getConnection() 
    {
      if (connection == null) 
      {
        connection = new NpgsqlConnection(connString);
        connection.Open();
      }
      return connection;
    }

    public static void closeConnection() {
      connection.Close();
    }
  }
}
