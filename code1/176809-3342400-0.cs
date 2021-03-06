            DataTable dataTable = null;
            try
            {
                ConnectionStringSettings mConString = ConfigurationManager.ConnectionStrings["SiteSqlServer"];
                using (SqlConnection connection = new SqlConnection(mConString.ConnectionString))
                {
                    connection.Open();
                    // Delete old entries
                    SqlCommand truncate = new SqlCommand("TRUNCATE TABLE MYTABLE", connection);
                    truncate.ExecuteNonQuery();
                }
                SqlBulkCopy bulkCopy = new SqlBulkCopy(mConString.ConnectionString, SqlBulkCopyOptions.TableLock)
                                              {
                                                  DestinationTableName = "dbo.MYTABLE",
                                                  BatchSize = 100000,
                                                  BulkCopyTimeout = 360
                                              };
                bulkCopy.WriteToServer(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
