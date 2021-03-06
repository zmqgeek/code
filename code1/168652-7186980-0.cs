      using (SqlConnection sqlconn = new SqlConnection())
            {
                sqlconn.ConnectionString = @"Data Source = #your_serwer\instance#; Initial Catalog = #db_name#; User Id = #user#; Password = #pwd#;";
                sqlconn.Open();
                // select your date and a key index field on a table (e.g. "id")
                string sql = "select id, some_date from table_1 where id = 1 ";
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlconn))
                {
                    using (DataTable table = new DataTable())
                    {
                        adapter.Fill(table);                       
                        DataRow r = null;
                        if (table.Rows.Count == 1)
                            r = table.Rows[0];   // case, when there is an row for update
                        else
                            r = table.NewRow();  // case, when there is nor row, and you would like to insert one
                        r.BeginEdit();
                        // here you assign your value
                        r["some_date"] = dateTimePicker.Value;
                        r.EndEdit();
                        // this will update the database
                        SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(adapter);
                        adapter.Update(table);
                    }
                }
            }
