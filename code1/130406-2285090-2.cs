    public sealed class NativeDisposable : IDisposable {
    
      public void Dispose() {
        CleanUpNativeResource();
        GC.SuppressFinalize(this);
      }
    
      protected virtual void CleanUpNativeResource() {
        // ...
      }
    
      ~NativeDisposable() {
        CleanUpNativeResource();
      }
    
      // ...
    
      IntPtr _nativeResource;
    
    }
