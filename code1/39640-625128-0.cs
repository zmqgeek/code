        public class SomeType 
        {
            [XmlElement]
            public int IntValue;
            [XmlIgnore]
            public bool Value;
            [XmlElement("Value")]
            public string Value_Surrogate {
                get { return (Value)? "Yes, definitely!":"Absolutely NOT!"; }
                set { Bool1= (value=="Yes, definitely!"); }
            }
        }
