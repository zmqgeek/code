    public static IEnumerable<DateRange> GetRanges(this IList<DateTime> dates)
    {
        List<DateRange> ranges = new List<DateRange>();
        DateRange currentRange = null;
        // this presumes a list of dates ordered by day, if not then the list will need sorting first
        for( int i = 0; i < dates.Count; ++i )
        {
            var currentDate = dates[i];
            if( i == 0 || dates[i - 1] != currentDate.AddDays(-1))
            {
                // it's either the first date or the current date isn't consecutive to the previous so a new range is needed
                currentRange = new DateRange();
                ranges.Add(currentRange);
            }
            currentRange.Add(currentDate);
        }
        return ranges;
    }
