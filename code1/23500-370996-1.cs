    using System;
    using System.Threading;
    
    namespace CA64213434234
    {
        class Program 
        {
            static void Main(string[] args)
            {
                ManualResetEvent ev = new ManualResetEvent(false);
                Foo bar = new Foo(0);
                Action a =  () => bar.Display(ev);
                IAsyncResult ar = a.BeginInvoke(null, null);
                ev.WaitOne();
                bar = new Foo(5);
                ar.AsyncWaitHandle.WaitOne();
            }
        }
    
        public struct Foo
        {
            private readonly int val;
            public Foo(int value)
            {
                val = value;
            }
            public void Display(ManualResetEvent ev)
            {
                Console.WriteLine(val);
                ev.Set();
                Thread.Sleep(2000);
                Console.WriteLine(val);
            }
        }
    }
