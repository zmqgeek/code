    public abstract class ClassA<T> where T: ClassA
    {
        public virtual T DoSomethingAndReturnNewObject();
    }
    
    public class ClassB: ClassA<ClassB>
    {
        public overrides ClassB DoSomethingAndReturnNewObject()
        {
            //do whatever
        }
    }
