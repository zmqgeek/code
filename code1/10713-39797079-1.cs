    string name = Globals.s_Name;
            string klanten = txt.Text;
            string s = klanten;
            XmlDocument xdoc = new XmlDocument();
            string klant = "<voornaam>" + naamBox1.Text + "</voornaam>";
            xdoc.LoadXml(s);
            XmlDocumentFragment xfrag = xdoc.CreateDocumentFragment();
            xfrag.InnerXml = klant;
            xdoc.DocumentElement.FirstChild.AppendChild(xfrag);
            xdoc.Save(name + "/klanten.xml");
