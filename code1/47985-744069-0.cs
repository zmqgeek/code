    SqlConnection connection = new SqlConnection("connection string");
    SqlCommand cmd = new SqlCommand("SELECT * FROM SomeTable", connection);
    SqlDataReader reader = cmd.ExecuteReader();
    if (reader != null)
    {
          while (reader.Read())
          {
                  //do something
          }
    }
    reader.Close(); // <- too easy to forget
    reader.Dispose(); // <- too easy to forget
