    private void General_KeyDown(object sender, KeyPressEventArgs e)
     {
     if (e.KeyCode == Keys.Enter)
            {
    
                if (this.GetNextControl(ActiveControl, true) != null)
                {
                    e.Handled = true;
                    this.GetNextControl(ActiveControl, true).Focus();
                }
            }
    }
