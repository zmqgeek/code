    // Instead of adding and removing callbacks manually and having a bunch of delegate types declared everywhere:
    
    public delegate void ObjectCallback(ObjectType broadcaster);
    
    public class Object
    {
    	public event ObjectCallback m_ObjectCallback;
    	
    	void SetupListener()
        {
    		ObjectCallback callback = null;
    		callback = (ObjectType broadcaster) =>
    		{
    			// one time logic here
    			broadcaster.m_ObjectCallback -= callback;
    		};
    		m_ObjectCallback += callback;
    
        }
    	
    	void BroadcastEvent()
    	{
    		m_ObjectCallback?.Invoke(this);
    	}
    }
    //(^Hard way)
    	
    // You could try this generic approach:
    
    public class Object
    {
    	public Broadcast<Object> m_EventToBroadcast = new Broadcast<Object>();
    	
    	void SetupListener()
    	{
    		m_EventToBroadcast.SubscribeOnce((ObjectType broadcaster) => {
    			// one time logic here
    		});
    	}
    	
    	void BroadcastEvent()
    	{
    		m_EventToBroadcast.Broadcast(this);
    	}
    }
    	
    	
    public delegate void ObjectDelegate<T>(T broadcaster);
    public class Broadcast<T>
    {
    	private event ObjectDelegate<T> m_Event;
    	private List<ObjectDelegate<T>> m_SingleSubscribers = new List<ObjectDelegate<T>>();
    
    	~Broadcast()
    	{
    		m_SingleSubscribers.Clear();
    		m_Event = delegate { };
    	}
    
    	// add a one shot to this delegate that is removed after first broadcast
    	public void SubscribeOnce(ObjectDelegate<T> del)
    	{
    		m_Event += del;
    		m_SingleSubscribers.Add(del);
    	}
    
    	// add a recurring delegate that gets called each time
    	public void Subscribe(ObjectDelegate<T> del)
    	{
    		m_Event += del;
    	}
    
    	public void Unsubscribe(ObjectDelegate<T> del)
    	{
    		m_Event -= del;
    	}
    
    	public void Broadcast(T broadcaster)
    	{
    		m_Event?.Invoke(broadcaster);
    		for (int i = 0; i < m_SingleSubscribers.Count; ++i)
    		{
    			Unsubscribe(m_SingleSubscribers[i]);
    		}
    		m_SingleSubscribers.Clear();
    	}
    }
