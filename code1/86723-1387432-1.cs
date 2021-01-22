    private IQueryable<IEvent> getEvents<T>(IEnumerable<int> IDs)
    	where T : class, IEvent
    {
    	var db = new EventDataContext();
    	return db.GetTable<T>.Where(e => IDs.Contains(e.ID)).Cast<IEvent>();
    }
