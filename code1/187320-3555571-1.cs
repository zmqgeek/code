    protected void Button_Click(object sender, EventArgs e)
    {
        string arg = ((Button)sender).CommandArgument;  // = MethodA
        switch (arg)
        {
            case "MethodA":
                MethodA();
            case "MethodB":
                MethodB();
        }
    }
