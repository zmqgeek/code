    public static class Extensions
    {
    
        public static string JoinStrings(this DataReader reader, int ColumnIndex, string delimiter)
        {
            var result = new StringBuidler();
            var delim = String.Empty;
            while (reader.Read())
            {
               result.Append(reader[ColumnIndex].ToString()).Append(delim);
               delim = delimiter;
            }
            return result.ToString();
        }
    }
