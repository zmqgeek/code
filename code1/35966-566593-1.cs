    static void Main(string[] args)
    {
        string xml = @"
                    <Items>
                        <Item>
                            <Stuff>Strings</Stuff>
                        </Item>
                        <Item>
                            <Stuff>Strings</Stuff>
                        </Item>
                    </Items>";
    
        using (StringReader myStream = new StringReader(xml))
        {
            XDocument doc = XDocument.Load(myStream);
    
            var query = from node in doc.Descendants("Item")
                        select new { Stuff = node.Element("Stuff").Value };
    
            foreach (var item in query)
            {
                Console.WriteLine("Stuff: {0}", item.Stuff);
            }
        }
    }
