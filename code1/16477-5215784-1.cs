     if(this.Dispatcher.CheckAccess())
    {
        this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate{
            this.Name = value; // Call same setter, but on the UI thread
        });
        return;
    }
