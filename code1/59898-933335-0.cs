    private int _myProperty;
    private bool _isMyPropertySet = false;
    public int MyProperty
    {
        set
        {
            if (this.DesignMode || !_isMyPropertySet)
            {
                    _isMyPropertySet = true;
                    _myProperty = value;
            }
            else
            {
                    throw new NotSupportedException();
            }
        }
    }
