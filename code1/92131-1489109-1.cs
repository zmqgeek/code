    public class MyClass {
        public delegate string MyDelegate();
        public string MyMethod1() {
            return "Hello";
        }
        public string MyMethod2() {
            return "Bye";
        }
    }
    int i;
    MyClass myInstance;
    MethodInfo method = typeof(MyClass).GetMethod("MyMethod" + i.ToString());
    MyClass.MyDelegate del = Delegate.CreateDelegate(typeof(MyClass.MyDelegate), myInstance, method);
    Console.WriteLine(del()); // prints "Hello" or "Bye" contingent on value of i 
