    object PropertyValue = ...
    if(Convert.GetTypeCode(PropertyValue) != TypeCode.Object)
    {
        string StringValue = Convert.ToString(PropertyValue);
        ...
    }
