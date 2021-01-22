    public class Highlighter
    {        
        private class Packet
        {
            public string Sentence;
            public double Density;
            public int Offset;
        }
        public static string FindSnippet(string text, string query, int maxLength)
        {
            if (maxLength < 0)
            {
                throw new ArgumentException("maxLength");
            }
            var words = query.Split(' ').Where(w => !string.IsNullOrWhiteSpace(w)).Select(word => word.ToLower()).ToLookup(s => s);             
            var sentences = text.Split('.');
            var i = 0;
            var packets = sentences.Select(sentence => new Packet 
            { 
                Sentence = sentence, 
                Density = ComputeDensity(words, sentence),
                Offset = i++
            }).OrderByDescending(packet => packet.Density);
            var list = new SortedList<int, string>();            
            int length = 0;                
            foreach (var packet in packets)
            {
                if (length >= maxLength || packet.Density == 0)
                {
                    break;
                }
                string sentence = packet.Sentence;
                list.Add(packet.Offset, sentence.Substring(0, Math.Min(sentence.Length, maxLength - length)));
                length += packet.Sentence.Length;
            }
            var sb = new List<string>();
            int previous = -1;
            foreach (var item in list)
            {
                var offset = item.Key;
                var sentence = item.Value;
                if (previous != -1 && offset - previous != 1)
                {
                    sb.Add(".");
                }
                previous = offset;             
                sb.Add(Highlight(sentence, words));                
            }
            return String.Join(".", sb);
        }
        private static string Highlight(string sentence, ILookup<string, string> words)
        {
            var sb = new List<string>();
            var ff = true;
            foreach (var word in sentence.Split(' '))
            {
                var token = word.ToLower();
                if (ff && words.Contains(token))
                {
                    sb.Add("[[HIGHLIGHT]]");
                    ff = !ff;
                }
                if (!ff && !string.IsNullOrWhiteSpace(token) && !words.Contains(token))
                {
                    sb.Add("[[ENDHIGHLIGHT]]");
                    ff = !ff;
                }
                sb.Add(word);
            }
            if (!ff)
            {
                sb.Add("[[ENDHIGHLIGHT]]");
            }
            return String.Join(" ", sb);
        }
        private static double ComputeDensity(ILookup<string, string> words, string sentence)
        {            
            if (string.IsNullOrEmpty(sentence) || words.Count == 0)
            {
                return 0;
            }
            int numerator = 0;
            int denominator = 0;
            foreach(var word in sentence.Split(' ').Select(w => w.ToLower()))
            {
                if (words.Contains(word))
                {
                    numerator++;
                }
                denominator++;
            }
            if (denominator != 0)
            {
                return (double)numerator / denominator;
            }
            else
            {
                return 0;
            }
        }
    }
