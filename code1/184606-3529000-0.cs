    			Process process = new Process();
			process.EnableRaisingEvents = true;
			process.StartInfo = new ProcessStartInfo();
			process.StartInfo.Arguments = "-update";
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.ErrorDialog = false;
			process.StartInfo.FileName = "updater.exe";
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
			process.Start();