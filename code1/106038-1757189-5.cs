    DateTime fileDate, closestDate;
    ArrayList theDates;
    int min = int.MaxValue;
    
    foreach (DateTime date in theDates)
     if (Math.Abs(date.Ticks- fileDate.Ticks) < min)
     {
       min = date.Ticks- fileDate.Ticks;
       closestDate = date;
     }
