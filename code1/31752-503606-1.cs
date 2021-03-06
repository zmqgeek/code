    private Timer _timer;
    private DateTime _lastRun = DateTime.Now;
    
    protected override void OnStart(string[] args)
    {
    	_timer = new Timer(60 * 1000);
    	_timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
    	//...
    }
    
    
    private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
    	// ignore the time, just compare the date
    	if (_lastRun.Date < DateTime.Now.Date)
    	{
    		// stop the timer while we are running the cleanup task
    		_timer.Stop();
    		//
    		// do cleanup stuff
    		//
    		_lastRun = DateTime.Now;
    		_timer.Start();
    	}
    }
