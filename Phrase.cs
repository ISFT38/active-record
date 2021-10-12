namespace active_record
{
  using System;
  using Npgsql;
  using System.Collections.Generic;

  class Phrase
  {
    public int?   PhraseId   { set; get; }
    public string PhraseText { set; get; }
    public int    PersonId   { set; get; }

    public void Save()
    {
      string sql;
      if(PhraseId == null) 
      {
        sql = "INSERT INTO phrase (phrase_text, person_id) VALUES (@p, @pi)";
        using (var command = new NpgsqlCommand(sql, Database.getConnection()))
        {
        command.Parameters.AddWithValue("p",  PhraseText);
        command.Parameters.AddWithValue("pi", PersonId);
        command.ExecuteNonQuery();
        }
        
      } else {
        sql = "UPDATE phrase SET phrase_text = @p, person_id = @pi WHERE phrase_id = @hi";
        using (var command = new NpgsqlCommand(sql, Database.getConnection()))
        {
          command.Parameters.AddWithValue("p",  PhraseText);
          command.Parameters.AddWithValue("pi", PersonId);
          command.Parameters.AddWithValue("hi", PhraseId);
          command.ExecuteNonQuery();
        }
      }
    }

    public Phrase getById(int id) {
      string sql = "SELECT phrase_id, phrase_text, person_id FROM phrase WHERE phrase_id = @pid";
      using( var command = new NpgsqlCommand(sql, Database.getConnection())) 
      {
        command.Parameters.AddWithValue("pid",  id);
        using (var reader = command.ExecuteReader())
          while (reader.Read())
          {
            Phrase phrase = new Phrase();
            phrase.PhraseId   = (int) reader.GetValue(0);
            phrase.PhraseText = reader.GetString(1);
            phrase.PersonId   = (int) reader.GetValue(2);
            return phrase;
          }
      }
      return null;
    }

    public List<Phrase> getByPersonId(int id) {
      return null; // TODO implement
    }

    public void Delete() {
      // TODO implement
    }

    public void Print() {
      Console.WriteLine(PhraseText);
    }

  }
}
