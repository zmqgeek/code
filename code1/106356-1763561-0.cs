    public class Worker
    {
        private static object _syncRoot = new object();
        private Thread _workerThread;
        private string _name;
    
        public Worker(string name)
        {
            _name = name;
            _workerThread = new Thread(new ThreadStart(DoWork));
            _workerThread.Start();
        }
        private void DoWork()
        {
            while(true)
            {
                lock(_syncRoot)
                {
                    Console.WriteLine(_name + "Doing Work");
                }
            }
        }
    }
