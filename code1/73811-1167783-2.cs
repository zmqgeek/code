    public static T FindControl<T>(this Control parent, string controlName) where T: Control
    {
        T found = parent.FindControl(controlName) as T;
        if (found != null)
           return found;
    
        foreach(Control childControl in parent.Controls)
        {
            found = childControl.FindControl<T>(controlName) as T;
            if (found != null)
               break;
        }
        
        return found;
    }
