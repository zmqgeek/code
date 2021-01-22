    void Main()
    {
        var client = new Client();
        NullSafeSet("test", s => client.WorkPhone = s);
        Debug.Assert(person.Name == "test");
        NullSafeSet("", s => client.WorkPhone = s);
        Debug.Assert(person.Name == "test");
        NullSafeSet(null, s => client.WorkPhone = s);
        Debug.Assert(person.Name == "test");
    }
    void NullSafeSet(string value, Action<string> setter)
    {
        if (!string.IsNullOrEmpty(value))
        {
            setter(value);
        }
    }
