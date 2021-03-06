    using (System.Transactions.TransactionScope ts = new TransactionScope())
    {
        using (SharedDbConnectionScope scs = new SharedDbConnectionScope())
        {
            try
            {
                //do your stuff like saving multiple objects etc. here 
                //everything should be completed nicely before you reach this
                //line if not throw exception and don't reach to line below
                ts.Complete();
            }
            catch (Exception ex)
            {
                //ts.Dispose(); //Don't need this as using will take care of it.
                //Do stuff with exception or throw it to caller
            }
        }
    }
