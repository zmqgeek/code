    TextWriter myTextWriter = new StringWriter();
    HtmlTextWriter myWriter = new HtmlTextWriter(myTextWriter);
    
    UserControl myDuplicate = new UserControl();
    TextBox blankTextBox;
    
    foreach (Control tmpControl in this.Controls)
    {
        switch (tmpControl.GetType().ToString())
        {
            case "System.Web.UI.WebControls.TextBox": // can we reuse this switch???
                blankTextBox = new TextBox();
                blankTextBox.ID = ((TextBox)tmpControl).ID;
                blankTextBox.Text = ((TextBox)tmpControl).Text;
                myDuplicate.Controls.Add(blankTextBox);
                break;
                // ...other types of controls (ddls, checkboxes, etc.)
        }
    }
    
    myDuplicate.RenderControl(myWriter);
    return myTextWriter.ToString();
