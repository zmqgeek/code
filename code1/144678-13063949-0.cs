    foreach (DataGridViewColumn dc in dataGridViewX1.Columns)
    {
           if (dc.Index.Equals(0))
           {
               dc.ReadOnly = false;
           }
           else if (dc.Index.Equals(1))
           {
                dc.ReadOnly = false;
           }
           else
           {
                dc.ReadOnly = true;
           }
     }
