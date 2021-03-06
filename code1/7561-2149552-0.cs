        public static string PureAscii(this string source, char nil = ' ')
        {
            var min = '\u0000';
            var max = '\u007F';
            return source.Select(c => c < min ? nil : c > max ? nil : c).ToString();
        }
        public static string ToString(this IEnumerable<char> source)
        {
            var buffer = new StringBuilder();
            foreach (var c in source)
                buffer.Append(c);
            return buffer.ToString();
        }
