    bool _restart = false;
    
    private void button1_Click(object sender, EventArgs e)
    {
        bgw.CancelAsync();
        _restart = true;
    }
    
    private void bgw_DoWork(object sender, DoWorkEventArgs e)
    {
    
        for (int i = 0; i < 300; i++)
        {
            if (bgw.CancellationPending)
            {
                break;
            }
            //time consuming calculation
        }
    }
    
    private void bgw_WorkComplete(object sender, eventargs e)  //no ide to hand not sure on name/args
    {
        if (_restart)
        {
            bgw.RunWorkerAsync();
            _restart = false;
        }
    
    }
