    public static class DateTimeHelper
        {
            private const int SECOND = 1;
            private const int MINUTE = 60 * SECOND;
            private const int HOUR = 60 * MINUTE;
            private const int DAY = 24 * HOUR;
            private const int MONTH = 30 * DAY;
    
            /// <summary>
            /// Returns a friendly version of the provided DateTime, relative to now. E.g.: "2 days ago", or "in 6 months".
            /// </summary>
            /// <param name="dateTime">The DateTime to compare to Now</param>
            /// <returns>A friendly string</returns>
            public static string GetFriendlyRelativeTime(DateTime dateTime)
            {
                if (DateTime.UtcNow.Ticks == dateTime.Ticks)
                {
                    return "Right now!";
                }
    
                bool isFuture = (DateTime.UtcNow.Ticks < dateTime.Ticks);
                var ts = DateTime.UtcNow.Ticks < dateTime.Ticks ? new TimeSpan(dateTime.Ticks - DateTime.UtcNow.Ticks) : new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
    
                double delta = ts.TotalSeconds;
    
                if (delta < 1 * MINUTE)
                {
                    return isFuture ? "in " + (ts.Seconds == 1 ? "one second" : ts.Seconds + " seconds") : ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
                }
                if (delta < 2 * MINUTE)
                {
                    return isFuture ? "in a minute" : "a minute ago";
                }
                if (delta < 45 * MINUTE)
                {
                    return isFuture ? "in " + ts.Minutes + " minutes" : ts.Minutes + " minutes ago";
                }
                if (delta < 90 * MINUTE)
                {
                    return isFuture ? "in an hour" : "an hour ago";
                }
                if (delta < 24 * HOUR)
                {
                    return isFuture ? "in " + ts.Hours + " hours" : ts.Hours + " hours ago";
                }
                if (delta < 48 * HOUR)
                {
                    return isFuture ? "tomorrow" : "yesterday";
                }
                if (delta < 30 * DAY)
                {
                    return isFuture ? "in " + ts.Days + " days" : ts.Days + " days ago";
                }
                if (delta < 12 * MONTH)
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return isFuture ? "in " + (months <= 1 ? "one month" : months + " months") : months <= 1 ? "one month ago" : months + " months ago";
                }
                else
                {
                    int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                    return isFuture ? "in " + (years <= 1 ? "one year" : years + " years") : years <= 1 ? "one year ago" : years + " years ago";
                }
            }
        }
