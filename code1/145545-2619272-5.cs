    public void Test()
    {
        FileStream stream = new FileStream(....);
        stream.Write(...);
        SomeOtherMethod();
        GC.KeepAlive(stream);
    }
