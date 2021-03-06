    public class PersonCollection : IEnumerable
    {
        private ArrayList alPers = new ArrayList();
        public IEnumerator GetEnumerator() { return new myTypeEnumerator(this); }
        public class myTypeEnumerator : IEnumerator
        {
            int nIndex;
            PersonCollection pers;
            public myTypeEnumerator(PersonCollection myTypes) 
             { pers = myTypes; nIndex = -1; }
            public bool MoveNext() { return (++nIndex < pers.alPers.Count); }
            // MovePrev() not strictly required
            public bool MovePrev() { return (--nIndex > pers.alPers.Count); }
            public object Current { get { return (pers[nIndex]); } }
            public void Reset() { nIndex = -1; }
        }
    }
