    myConStr = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
    using (var cn = new SqlConnection(myConStr) )
    using (var cmd = new SqlCommand("team5UserCurrentBooks3", cn) ) 
    {
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@book_id", SqlDbType.Int).Value = bookID;
        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userID;
    
        cn.Open();
        cmd.ExecuteNonQuery();
    }
