    /// <summary>
    ///     'Helps' protect against XSS (Cross Site Scripting attacks) by stripping out known evil HTML elements
    ///     such as script and style. Used for outputing text generated by a Rich Text Editor. Doesn't HTML encode!
    /// </summary>
    /// <param name="input">Input string to strip bad HTML elements from</param>
    public static string XSSProtect(string input)
    {
        string returnVal = input ?? "";
    
        returnVal = Regex.Replace(returnVal, @"\<script(.*?)\>(.*?)\<\/script(.*?)\>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        returnVal = Regex.Replace(returnVal, @"\<style(.*?)\>(.*?)\<\/style(.*?)\>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    
        while (Regex.IsMatch(returnVal, @"(<[\s\S]*?) on.*?\=(['""])[\s\S]*?\2([\s\S]*?>)", RegexOptions.Compiled | RegexOptions.IgnoreCase))
        {
            returnVal = Regex.Replace(returnVal, @"(<[\s\S]*?) on.*?\=(['""])[\s\S]*?\2([\s\S]*?>)",
                            delegate(Match match)
                            {
                                return String.Concat(match.Groups[1].Value, match.Groups[3].Value);
                            }, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    
        return returnVal;
    }