    class A:ICloneable
    {
        internal MyClass _parent;
        
        public MyClass Parent
        {
            get
            {
                return this._parent;
            }
        }
    
    #region ICloneable Members
    
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    
    #endregion
    
        .....some methods and proeprties
    }
    
    class MyType
    {
        private A _myProperty;
    
        public A MyProperty
        {
            get
            {
                    return this._myProperty;
            }
            set
            {
                    this._myProperty = a.Clone();
                    this._myProperty._parent = this;
            }
        }
    }
