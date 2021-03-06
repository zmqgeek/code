    public sealed class DataCounter
    {
        private class CounterBox
        {
            public int Count { get; set; }
        }
        public event EventHandler NewKeyEvent;
        public event EventHandler ZeroCountEvent;
        private readonly Dictionary<string, CounterBox> _data
            = new Dictionary<string, CounterBox>();
    
        public void Register(string dataKey)
        {
            lock (_data)
            {
                if (_data.ContainsKey(dataKey))
                {
                    _data[dataKey].Count++;
                }
                else
                {
                    _data.Add(dataKey, new CounterBox { Count = 1 });
                    if (NewKeyEvent != null) NewKeyEvent(this, null);
                }
            }
        }
    
        public void Deregister(string dataKey)
        {
            lock (_data)
            {
                CounterBox box;
                if (_data.TryGetValue(dataKey, out box))
                {
                    if (box.Count > 0)
                    {
                        box.Count--;
                    }
    
                    if (box.Count == 0)
                    {
                        EventHandler handler = ZeroCountEvent;
                        if (handler != null) handler(this, EventArgs.Empty);
                        _data.Remove(dataKey);
                    }
                }
            }
        }
    }
