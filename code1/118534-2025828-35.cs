        private static void Main() {
            // run RetrieveJob every 5 minutes using a timer
            Timer timer = new Timer(RetrieveJob);
            timer.Change(TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(5));
            // run ProcessJobs in thread
            Thread thread = new Thread(ProcessJobs);
            thread.Run();
            // block main thread
            Console.ReadLine();
            // stop the timer
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            // add 'null' job and wait until ProcessJobs has finished
            EnqueueJob(null);
            thread.Join();
        }
