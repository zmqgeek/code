    Size newSize = new Size(27, 20);
    foreach (Control c in this.Controls)
    {
       if (c is TextBox and c.Name.EndsWith("txt2"))
       {
          c.Size = newSize;
       }
    }
