    abstract class A
    {
        public abstract string X
        {
            get;
        }
    }
    class A1 : A
    {
        public override string X
        {
            get { return "A1"; }
        }
    }
    class A2 : A
    {
        public override string X
        {
            get { return "A2"; }
        }
    }
