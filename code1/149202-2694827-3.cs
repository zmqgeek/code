    class Test
    {
        int i;
        static int si=9; 
    
        public Test()
        {
            i = 199;
        }
 
        static void main()
        {
            for (int i = 0; i < 10; i++)
            {
                var x = 2;
            }
            { var x = 3; }
            {    // remove outer "{ }" will generate compile error
                int si = 3; int i = 0;
                 Console.WriteLine(si);
                 Console.WriteLine(Test.si);
                 Console.WriteLine(i);
                 Console.WriteLine((new Test()).i);
            }
        }
    }
