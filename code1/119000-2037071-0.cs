        Dim o As New DataTable("testTable")
        o.Columns.Add("TestCol")
        o.Rows.Add(New Object() {"Testvalue1"})
        o.Rows.Add(New Object() {"Testvalue2"})
        Dim oSet As New DataSet()
        oSet.Tables.Add(o)
        MessageBox.Show(oSet.GetXml) 