    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            username.Value = Request["username"];
        }
    }
