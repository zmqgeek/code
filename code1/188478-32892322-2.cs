    public int NormalProperty
    {
        get {return _model.modelProperty; }
        set 
        {
            _model.modelProperty = value;
            OnPropertyChanged();
        }
    }
    [DependsOnProperty("NormalProperty")]
    public int CalculatedProperty
    {
        get { return _model.modelProperty + 1; }
    }
