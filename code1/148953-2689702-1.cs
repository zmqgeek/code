    private int panelIndex = 0;
    void SomeButton_Click(object sender, EventArgs e)
    {
       if (this.Controls.Contains(Panel2))
       {
          panelIndex = this.Controls.IndexOf(Panel2);
          this.Controls.Remove(Panel2);
       }
       else
       {
          IList ctrlsAsList = (IList)this.Controls;
          ctrlsAsList.Insert(panelIndex, Panel2);
       }
    }
