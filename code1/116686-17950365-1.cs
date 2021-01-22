    XmlNode myXmlNodeObject = myXmlDocService.GetData(_xmlDataString);
    
    //Load the data from the returned XmlNode into an XmlDocument Object
    XmlDocument myXmlDocumentObject = new XmlDocument();
    myXmlDocumentObject.AppendChild(myXmlDocumentObject.ImportNode(myXmlNodeObject, true));
	//Bind To GridView
	if (myXmlNodeObject.ChildNodes.Item(0).InnerText.Contains("Query Successful"))
	{
		//Create a DataSet To Bind To
		DataSet ds = new DataSet();
		ds.Tables.Add("XmlDataSet");
		//Get Column Names as String Array
		XmlDocument XMLDoc = new XmlDocument();
		XMLDoc.LoadXml("<result>" +myXmlNodeObject.ChildNodes.Item(0).ChildNodes.Item(2).ParentNode.InnerXml + "</result>"); //Get Row/Columns
		int colCount = myXmlNodeObject.ChildNodes.Item(0).ChildNodes.Item(2).SelectNodes("column").Count;
		string[] ColumnNameArray = new string[colCount];
		int iterator = 0;
		foreach(XmlNode node in myXmlNodeObject.ChildNodes.Item(0).ChildNodes.Item(2).SelectNodes("column"))
		{
			ColumnNameArray.SetValue(node.Attributes["name"].Value ,iterator);
			ds.Tables["XmlDataSet"].Columns.Add(node.Attributes["name"].Value); //Create individual columns in the dataset
			iterator++;
		}
		//Get Data Row By Row to populate the DataSet.Rows
		foreach(XmlNode RowNode in XMLDoc.ChildNodes.Item(0).SelectNodes("row"))
		{
			string[] rowArray = new string[colCount]; 
			int iterator2 = 0;
			foreach(XmlNode ColumnNode in RowNode.ChildNodes)
			{
				rowArray.SetValue(ColumnNode.InnerText, iterator2);
				iterator2++;
			}
			ds.Tables["XmlDataSet"].Rows.Add(rowArray);
		}
		DataGridView.DataSource = ds.Tables["XmlDataSet"];
		DataGridView.DataKeyNames = ColumnNameArray;
		DataGridView.DataBind();
		DataGridView.Visible = true;
	}
