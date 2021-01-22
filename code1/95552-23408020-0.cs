        public static IEnumerable<string> SplitToLines(this string input)
        {
            if (input == null)
            {
                yield break;
            }
            using (System.IO.StringReader reader = new System.IO.StringReader(input))
            {
                string line;
                while( (line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
