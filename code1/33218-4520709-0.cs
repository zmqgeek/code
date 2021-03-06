    ProcessStartInfo startInfo = new ProcessStartInfo();
    startInfo.FileName = @"powershell.exe";
    startInfo.Arguments = @"& 'c:\Scripts\test.ps1'";
    startInfo.RedirectStandardOutput = true;
    startInfo.RedirectStandardError = true;
    startInfo.UseShellExecute = false;
    startInfo.CreateNoWindow = true;
    Process process = new Process();
    process.StartInfo = startInfo;
    process.Start();
    string output = process.StandardOutput.ReadToEnd();
    Assert.IsTrue(output.Contains("StringToBeVerifiedInAUnitTest"));
    string errors = process.StandardError.ReadToEnd();
    Assert.IsTrue(string.IsNullOrEmpty(errors));
