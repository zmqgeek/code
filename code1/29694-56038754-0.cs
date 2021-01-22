		public static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
		{
			var tcs = new TaskCompletionSource<bool>();
			void Process_Exited(object sender, EventArgs e)
			{
				Task.Run(() => tcs.TrySetResult(true));
			}
			process.EnableRaisingEvents = true;
			process.Exited += Process_Exited;
			try
			{
				if (process.HasExited)
				{
					return;
				}
				using (cancellationToken.Register(() => Task.Run(() => tcs.TrySetCanceled())))
				{
					await tcs.Task;
				}
			}
			finally
			{
				process.Exited -= Process_Exited;
			}
		}
