    public static class CSV
    {
        public static IEnumerable<IList<string>> FromFile(string fileName)
        {
            foreach (IList<string> item in FromFile(fileName, ignoreFirstLineDefault)) yield return item;
        }
        public static IEnumerable<IList<string>> FromFile(string fileName, bool ignoreFirstLine)
        {
            using (StreamReader rdr = new StreamReader(fileName))
            {
                foreach(IList<string> item in FromReader(rdr, ignoreFirstLine)) yield return item;
            }
        }
        public static IEnumerable<IList<string>> FromStream(Stream csv)
        {
            foreach (IList<string> item in FromStream(csv, ignoreFirstLineDefault)) yield return item;
        }
        public static IEnumerable<IList<string>> FromStream(Stream csv, bool ignoreFirstLine)
        {
            using (StreamReader rdr = new StreamReader(csv))
            {
                foreach (IList<string> item in FromReader(rdr, ignoreFirstLine)) yield return item;
            }
        }
        public static IEnumerable<IList<string>> FromReader(StreamReader csv)
        {
            //Probably should have used TextReader instead of StreamReader
            foreach (IList<string> item in FromReader(csv, ignoreFirstLineDefault)) yield return item;
        }
        public static IEnumerable<IList<string>> FromReader(StreamReader csv, bool ignoreFirstLine)
        {
            if (ignoreFirstLine) csv.ReadLine();
            IList<string> result = new List<string>();
            StringBuilder curValue = new StringBuilder();
            char c;
            c = (char)csv.Read();
            while ((int)c > 0 && !csv.EndOfStream)
            {
                switch (c)
                {
                    case ',': //empty field
                        result.Add("");
                        c = (char)csv.Read();
                        break;
                    case '"': //qualified text
                        c = (char)csv.Read();
                        bool inQuotes = true;
                        while ((int)c > 0 && inQuotes && !csv.EndOfStream)
                        {
                            if (c == '"')
                            {
                                c = (char)csv.Read();
                                if (c != '"')
                                    inQuotes = false;
                            }
                            if (inQuotes)
                            {
                                curValue.Append(c);
                                c = (char)csv.Read();
                            } 
                        }
                        result.Add(curValue.ToString());
                        curValue = new StringBuilder();
                        if (c == ',') c = (char)csv.Read(); // either ',', newline, or endofstream
                        break;
                    case '\n': //end of the record
                    case '\r':
                        //potential bug here depending on what your line breaks look like
                        if (result.Count > 0) // don't return empty records
                        {
                            yield return result;
                            result = new List<string>();
                        }
                        c = (char)csv.Read();
                        break;
                    default: //normal unqualified text
                        while ((int)c > 0 && c != ',' && c != '\r' && c != '\n' && !csv.EndOfStream )
                        {
                            curValue.Append(c);
                            c = (char)csv.Read();
                        }
                        result.Add(curValue.ToString());
                        curValue = new StringBuilder();
                        if (c == ',') c = (char)csv.Read(); //either ',', newline, or endofstream
                        break;
                }
                
            }
            if (curValue.Length > 0)
                result.Add(curValue.ToString());
            if (result.Count > 0) 
                yield return result;
        }
        private static bool ignoreFirstLineDefault = false;
    }
