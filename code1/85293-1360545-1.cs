    public class Queue<T>
    {
    	/// <summary>Used as a lock target to ensure thread safety.</summary>
    	private readonly Locker _Locker = new Locker();
    
    	private readonly System.Collections.Generic.Queue<T> _Queue = new System.Collections.Generic.Queue<T>();
    
    	/// <summary></summary>
    	public void Enqueue(T item)
    	{
    		lock (_Locker)
    		{
    			_Queue.Enqueue(item);
    		}
    	}
    
    	/// <summary>Enqueues a collection of items into this queue.</summary>
    	public virtual void EnqueueRange(IEnumerable<T> items)
    	{
    		lock (_Locker)
    		{
    			if (items == null)
    			{
    				return;
    			}
    
    			foreach (T item in items)
    			{
    				_Queue.Enqueue(item);
    			}
    		}
    	}
    
    	/// <summary></summary>
    	public T Dequeue()
    	{
    		lock (_Locker)
    		{
    			return _Queue.Dequeue();
    		}
    	}
    
    	/// <summary></summary>
    	public void Clear()
    	{
    		lock (_Locker)
    		{
    			_Queue.Clear();
    		}
    	}
    
    	/// <summary></summary>
    	public Int32 Count
    	{
    		get
    		{
    			lock (_Locker)
    			{
    				return _Queue.Count;
    			}
    		}
    	}
    
    	/// <summary></summary>
    	public Boolean TryDequeue(out T item)
    	{
    		lock (_Locker)
    		{
    			if (_Queue.Count > 0)
    			{
    				item = _Queue.Dequeue();
    				return true;
    			}
    			else
    			{
    				item = default(T);
    				return false;
    			}
    		}
    	}
    }
