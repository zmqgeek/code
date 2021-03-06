    class Time
    {
        public int Hours   { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
    
        public Time(uint h, uint m, uint s)
        {
            if(h > 23 || m > 59 || s > 59)
            {
                throw new ArgumentException("Invalid time specified");
            }
            Hours = (int)h; Minutes = (int)m; Seconds = (int)s;
        }
    
        public Time(DateTime dt)
        {
            Hours = dt.Hour;
            Minutes = dt.Minute;
            Seconds = dt.Second;
        }
    
        public override string ToString()
        {  
            return String.Format(
                "{0:00}:{1:00}:{2:00}",
                this.Hours, this.Minutes, this.Seconds);
        }
        public void AddHours(uint h)
        {
            this.Hours += (int)h;
            if(this.Hours > 23)
                this.Hours -= 23;
        }
        public void AddMinutes(uint m)
        {
            this.Minutes += (int)m;
            if(this.Minutes > 59)
                this.Minutes -= 59;
        }
        public void AddSeconds(uint s)
        {
            this.Seconds += (int)s;
            if(this.Seconds> 59)
                this.Seconds-= 59;
        }
    }
