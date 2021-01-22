        lock (_tabs)
        {
            Type[] t = { typeof(tsgPublicDecs.tsgClsTab) };
            System.Xml.Serialization.XmlSerializer srl = new System.Xml.Serialization.XmlSerializer(typeof(ArrayList), t);
            using (System.IO.Stream ms = new System.IO.MemoryStream())
            {
                srl.Serialize(ms, _tabs);
                ms.Seek(0, 0);
                using (System.IO.TextReader sr = new System.IO.StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }
