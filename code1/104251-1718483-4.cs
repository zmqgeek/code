    private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            ContextMenu m = new ContextMenu();
            m.MenuItems.Add(new MenuItem("Cut"));
            m.MenuItems.Add(new MenuItem("Copy"));
            m.MenuItems.Add(new MenuItem("Paste"));
            int currentMouseOverRow = dataGridView1.HitTest(e.X,e.Y).RowIndex;
            
            if (currentMouseOverRow >= 0)
            {
                m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
            }
                
            m.Show(dataGridView1, new Point(e.X, e.Y));
        }
    }
