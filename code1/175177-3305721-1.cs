    using System.Threading;
        
    public class MyWorker {
            private ManualResetEvent mResetEvent;
            private volatile bool mIsAlive;
            private const int mTimeout = 6000000;
        
            public void Start()
            {
                if (mIsAlive == false)
                {
                    mIsAlive = true;
                    Thread thread = new Thread(new ThreadStart(RunThread));
                    thread.Start();
                }
            }
        
            public void Stop()
            {
                mIsAlive = false;
                mResetEvent.Set();
            }
        
            public void RunThread()
            {
                while(mIsAlive)
                {
                    //Reset the event -we may be restarting the thread.
                    mResetEvent.Reset();
                        
                    DoWork();
        
                    //The thread will block on this until either the timeout
                    //expires or the reset event is signaled.
                    mResetEvent.WaitOne(mTimeout);
                }
            }
        
            public void DoWork()
            {
                //...
            } }