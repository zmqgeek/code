    using System.Diagnostics.Process; 
    List <Process()> plist = new List<Process>();            
    
    foreach (Process p in Process.GetProcesses())
    {
     string title = p.MainWindowTitle;
     if (!String.IsNullOrEmpty(title))
     {
         plist.Add(p);
     }
    }
