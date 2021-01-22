    dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
	
	private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (dataGridView1.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox)
        {
            ComboBox comboBox = e.Control as ComboBox;
            comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
        }
    }
    private void LastColumnComboSelectionChanged(object sender, EventArgs e)
    {
        var sendingCB = sender as DataGridViewComboBoxEditingControl;
        int value = (int)sendingCB.SelectedValue;
        //do something with the value
    }
