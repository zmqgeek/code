     using(var connection = new SqlConnection(myConnectionString)){
          if(connection.CanOpen()){
           // NOTE: The connection is not open at this point...
           // You can either open it here or not close it in the extension method...
           // I prefer opening the connection explicitly here...
         }
    }
