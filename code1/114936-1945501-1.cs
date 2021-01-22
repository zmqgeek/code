    var _query = from _v in Classes.Data.getdb().vUsers
                 select v;
    
    if(!txtUsername.IsBlank())
        _query = _query.Where(x => x.username.Contains(txtUsername.Text));
    
    if(!txtFirstName.IsBlank())
        _query = _query.Where(x => x.firstname.Contains(txtFirstName.Text));
    
    // etc.
    
    gv.DataSource = _query;
