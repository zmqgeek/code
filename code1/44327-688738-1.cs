    public class SomeDelegate : Delegate
    {
        public SomeDelegate(Object target, IntPtr method) { ... }
        public virtual IAsyncResult BeginInvoke(
            AsyncCallback callback, object @object) { ... }
 
        public virtual void EndInvoke(IAsyncResult result) { ... }
        public virtual void Invoke() { ... }
    }
