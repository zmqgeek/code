    using System;
    using System.Threading;
    public class Example {
        public static void Main() {
    
            // Queue the task.
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));
    
            Thread.Sleep(1000);
    
            Console.WriteLine("Main thread exits.");
        }
    
        // This thread procedure performs the task. 
        static void ThreadProc(Object stateInfo) {
    
            // No state object was passed to QueueUserWorkItem, so  
            // stateInfo is null.
            Console.WriteLine("Hello from the thread pool.");
        }
    }
