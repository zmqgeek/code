    using(var session = _sessionFactory.GetCurrentSession()
    using(var tx = _session.BeginTransaction(){
        ... do something
        tx.Commit();
    }
    
    using(var session = _sessionFactory.GetCurrentSession()
    using(var tx = _session.BeginTransaction(){
        ... do something else
    }
