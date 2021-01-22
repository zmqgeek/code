    int activeRemindingTasks = 0;
    OutlookSession session = new OutlookSession();
    for (Task t in session.Tasks.Items)
    {
      if (t.ReminderSet && t.StartDate.AddMinutes(-t.ReminderTime) <= DateTime.Now)
      {
        activeRemindingTasks++;
      }
    }
