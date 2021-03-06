	using System.Text.RegularExpressions;
	static bool match(this string str, string pat, out Match m) =>
		(m = Regex.Match(str, pat, RegexOptions.IgnoreCase)).Success;
	static void Main()
	{
		Dictionary<string, Dictionary<string, string>> ini = new Dictionary<string, Dictionary<string, string>>();
		string section = "";
		foreach (string line in File.ReadAllLines(.........)) // read from file
		{
			string ln = (line.Contains('#') ? line.Remove(line.IndexOf('#')) : line).Trim();
			if (ln.match(@"\[(?<sec>[\w\-]+)\]", out Match m))
			    section = m.Groups["sec"].ToString();
			else if (ln.match(@"(?<prop>[\w\-]+)\=(?<val>.*)", out m))
                if (ini.ContainsKey(section))
                    ini[section][m.Groups["prop"].ToString()] = m.Groups["val"].ToString();
                else
                    ini[section] = new Dictionary<string, string>();
		}
		// access the ini file as follows:
		string content = ini["section"]["property"];
	}
