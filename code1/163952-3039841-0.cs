    string conString = "...";  // <-- your connection string
    using (IDbConnection connection = new OleDbConnection(conString))
    {
        connection.Open();
        try
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT PageIndex FROM RESUME";
                object scalar = command.ExecuteScalar();
                // ^ ExecuteScalar reads the first value in the first column
                //   of the result set; or returns null if it fails to do so.
                if (scalar == null) throw ...;  // unexpected result from database!
                int pIndex = (int)scalar;
                switch (pIndex)
                {
                    case 0:  Response.Redirect("Create Resume-1.aspx"); break;
                    case 1:  Response.Redirect("Create Resume-2.aspx"); break;
                    case 2:  Response.Redirect("Create Resume-3.aspx"); break;
                    default: throw ...;  // unexpected result from database!?
                }
            }
        }
        finally
        {
            connection.Close();  // (will execute even when exceptions are thrown!)
        }
    }
