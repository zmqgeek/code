    XmlTextWriter xmlw = new XmlTextWriter(@"C:\WINDOWS\Temp\exm.xml",System.Text.Encoding.UTF8);
    xmlw.WriteStartDocument();            
    xmlw.WriteStartElement("examtimes");
    xmlw.WriteStartElement("Starttime");
    xmlw.WriteString(DateTime.Now.AddHours(0).ToString());
    xmlw.WriteEndElement();
    xmlw.WriteStartElement("Changetime");
    xmlw.WriteString(DateTime.Now.AddHours(0).ToString());
    xmlw.WriteEndElement();
    xmlw.WriteStartElement("Endtime");
    xmlw.WriteString(DateTime.Now.AddHours(1).ToString());
    xmlw.WriteEndElement();
    xmlw.WriteEndElement();
    xmlw.WriteEndDocument();  
    xmlw.Close();           
