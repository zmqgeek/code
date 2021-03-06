    public class SerializableBase
    {
       public bool Property1 { get; set;}
       public bool Property3 { get; set;}
       public abstract bool copyOfProperty1 { get; set;}
       public abstract bool copyOfProperty3 { get; set;}
    }
    [XmlRoot("Object")]
    public class SerializableObject : SerializableBase
    {
      [XmlElement("Property1")]
      public override bool copyOfProperty1 
      { 
        get { return base.Property1; }
        set { base.Property1 = value; }
      }
      [XmlElement]
      public bool Property2 { get; set;}
      [XmlElement("Property3")]
      public override bool copyOfProperty3
      { 
        get { return base.Property3; }
        set { base.Property3 = value; }
      }
    }
