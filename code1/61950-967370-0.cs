    public bool IsValidSSN(object ssn) {
      ...
      IsValidSSN(Convert.ToInt32(ssn));
      ...
    }
    
    public bool IsValidSSN(int ssn) {
      ...
    }