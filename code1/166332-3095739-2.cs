    public void MyMethod()
    {
         StackTrace stackTrace = new System.Diagnostics.StackTrace();
         StackFrame frame = stackTrace.GetFrames()[1];
         MethodInfo method = frame.GetMethod();
         string methodName = method.Name;
         Type methodsClass = method.DeclaringType;
    }
