
namespace active_record
{
  using System;
  using Npgsql;
  /**
    * Los métodos de esta clase son, intencionalmente, susceptibles a la inyección de SQL
    * Se escribe de esta forma solamente con fines demostrativos.
    */
  class Person 
  {
    public int?     PersonId  { set; get; }
    public string   FirstName { set; get; } 
    public string   LastName  { set; get; }
    public DateTime Birth     { set; get; }

    public void Save()
    {
      string date = Birth.ToString("yyyy-MM-dd");
      string sql  = null;
      if (PersonId == null)
        sql = "INSERT INTO person(birth, first_name, last_name) VALUES ('" +
                     date + "', '" + FirstName + "', '" + LastName + "')";
      else
        sql = "UPDATE person SET first_name = '" + FirstName + "', last_name = '" + LastName +
                       "', birth = '" + date + "' WHERE person_id = " + PersonId;
      
      Console.WriteLine(sql);
      using (var command = new NpgsqlCommand(sql, Database.getConnection()))
      {
        command.ExecuteNonQuery();
      }
    }
    
    public static Person getById(int ID) {
      string sql = "SELECT person_id, first_name, last_name, birth FROM person WHERE person_id = " + ID;
      using( var command = new NpgsqlCommand(sql, Database.getConnection()))
        using (var reader = command.ExecuteReader())
          while (reader.Read())
          {
              Person p = new Person();
              p.PersonId  = (int) reader.GetValue(0);
              p.FirstName = reader.GetString(1);
              p.LastName  = reader.GetString(2);
              p.Birth     = reader.GetDateTime(3);
              return p;
          }
      return null;
    }

    public void Delete() {
      if (PersonId != null)
      {
        string sql = "DELETE FROM person WHERE person_id = " + PersonId;
        using (var command = new NpgsqlCommand(sql, Database.getConnection()))
        {
          command.ExecuteNonQuery();
        }
      }
    }

    public void Print() {
      Console.WriteLine( FirstName + " " + LastName + ", nacido el " + Birth.ToString("dd MMMM 'de' yyyy"));
    }
  }
}