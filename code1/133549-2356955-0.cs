        public class MyClass{
    public static List<MyClass> Instances = new List<MyClass>();
    
        public MyClass(){
    lock(typeof(MyClass)){
    Instances.Add(this);
    }
    
    }}
