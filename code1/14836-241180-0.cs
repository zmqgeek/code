    using System;
    using System.Threading;
    
    class Test
    {
        static void Main()
        {
            for (int i=0; i < 10; i++)
            {
                ThreadStart ts = delegate { Console.WriteLine(i); };
                new Thread(ts).Start();
            }
        }
    }
