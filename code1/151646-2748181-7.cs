    Dictionary<string, bool[]> userRoles = new Dictionary<string, bool[]>(){
        {"Bob", new bool[]{true,true,false,false}},
        {"Tim",new bool[]{false,false,true,true}},
        {"John",new bool[]{false,true,false,true}}
    };
    
    string[] headings = { "Rep Name", "Caller", "Closer", "Manager", "SuperUser" };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            rowRepeater.DataSource = userRoles;
            rowRepeater.DataBind();
        }
    }
    
    protected void rowRepeater_ItemBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Repeater headerRepeater = e.Item.FindControl("headerRepeater") as Repeater;
            headerRepeater.DataSource = headings;
            headerRepeater.DataBind();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater columnRepeater = e.Item.FindControl("columnRepeater") as Repeater;
            columnRepeater.DataSource = ((KeyValuePair<string, bool[]>)e.Item.DataItem).Value;
            columnRepeater.DataBind();
        }
    }
    
    protected void cbColumnValue_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = sender as CheckBox;
        HiddenField hf = cb.Parent.FindControl("hfRoleIndex") as HiddenField;
        int roleIndex = int.Parse(hf.Value); // now you have the column identifier
    
        Label lbl = cb.Parent.Parent.Parent.FindControl("lblUserName") as Label;
        string userName = lbl.Text; //now you have the row identifier
    
        //now you can update your data source
        userRoles[userName][roleIndex] = cb.Checked;
    }
