    void Main()
    {
    	new[] {"a4","a3","a2","a10","b5","b4","b400","1","C1d","c1d2"}.OrderBy(x => x, new NaturalStringComparer()).Dump();
    }
    
    public class NaturalStringComparer : IComparer<string>
    {
        private static readonly Regex _re = new Regex(@"(?<=\D)(?=\d)|(?<=\d)(?=\D)", RegexOptions.Compiled);
        public int Compare(string x, string y)
        {
            x = x.ToLower();
            y = y.ToLower();
            if(string.Compare(x, 0, y, 0, Math.Min(x.Length, y.Length)) == 0)
            {
                if(x.Length == y.Length) return 0;
                return x.Length < y.Length ? -1 : 1;
            }
            var a = _re.Split(x);
            var b = _re.Split(y);
            int i = 0;
            while(true)
            {
                int r = PartCompare(a[i], b[i]);
                if(r != 0) return r;
                ++i;
            }
        }
        private static int PartCompare(string x, string y)
        {
            int a, b;
            if(int.TryParse(x, out a) && int.TryParse(y, out b))
                return a.CompareTo(b);
            return x.CompareTo(y);
        }
    }
