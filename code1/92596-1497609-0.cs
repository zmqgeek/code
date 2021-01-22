    using System;
    using System.Globalization;
    
    
    public class SamplesCalendar  {
    
       public static void Main()  {
    
          // Gets the Calendar instance associated with a CultureInfo.
          CultureInfo myCI = new CultureInfo("en-US");
          Calendar myCal = myCI.Calendar;
    
          // Gets the DTFI properties required by GetWeekOfYear.
          CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
          DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
    
          // Displays the number of the current week relative to the beginning of the year.
          Console.WriteLine( "The CalendarWeekRule used for the en-US culture is {0}.", myCWR );
          Console.WriteLine( "The FirstDayOfWeek used for the en-US culture is {0}.", myFirstDOW );
          Console.WriteLine( "Therefore, the current week is Week {0} of the current year.", myCal.GetWeekOfYear( DateTime.Now, myCWR, myFirstDOW ));
    
          // Displays the total number of weeks in the current year.
          DateTime LastDay = new System.DateTime( DateTime.Now.Year, 12, 31 );
          Console.WriteLine( "There are {0} weeks in the current year ({1}).", myCal.GetWeekOfYear( LastDay, myCWR, myFirstDOW ), LastDay.Year );
    
       }
    
    }
