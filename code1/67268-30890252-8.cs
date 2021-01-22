    public class SectionSchool : ConfigurationSection
    {
        public School Content { get; set; }
    
    	protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
    	{
            var serializer = new XmlSerializer(typeof(School)); // works in     4.0
            // var serializer = new XmlSerializer(type, null, null, null, null); // works in 4.5.1
            Content = (Schoool)serializer.Deserialize(reader);
    	}
        public static School Load()
    	{
            var section = ConfigurationManager.GetSection("School") as SectionSchool;
        
    		if (section == null)
    			return null;
    
    		return section.Content;
    	}
    }
