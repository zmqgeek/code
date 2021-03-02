    string ToTime(double hours)
    {
        var span=TimeSpan.FromHours(value);
        return string.Format("{0}:{1:00}:{2:00}",
            (int)span.TotalHours, span.Minutes, span.Seconds);
    }
