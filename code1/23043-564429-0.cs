    Process proc = null;
    System.Diagnostics.ProcessStartInfo info;
    string domain = string.IsNullOrEmpty(row.Domain) ? "." : row.Domain;
    info = new ProcessStartInfo("Starter.exe");
    info.Arguments = cmd + " " + domain + " " + username + " " + password + " " + args;
    info.WorkingDirectory = Path.GetDirectoryName(cmd);
    info.UseShellExecute = false;
    info.RedirectStandardError = true;
    info.RedirectStandardOutput = true;
    proc = System.Diagnostics.Process.Start(info);