    string _firstName;
    public string FirstName
    {
    	get { return _firstName; }
    	set { OnPropertyChanged(ref _firstName, value, "FirstName", "FullName", Command1, Command2); }
    }
