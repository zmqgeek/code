    public string Concat(IEnumerable<string> stringList)
    {
        StringBuilder textBuilder;
        string separator = String.Empty;
        foreach(string item in stringList)
        {
            textBuilder.Append(item);
            textBuilder.Append(separator);
            separator = ", ";
        }
    }
