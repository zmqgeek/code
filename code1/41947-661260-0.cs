    class Foo
    {
        public static void Do()
        {
            Console.WriteLine("Foo.Do");
        }
    }
    
    class Bar:Foo // :Foo added
    {
        public static new void Do()
        {
            Console.WriteLine("Bar.Do");
        }
    }
