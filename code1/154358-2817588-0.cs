    public class MyControl : UserControl 
    {	
	    public static readonly DependencyProperty MyIntegerProperty = 
                 DependencyProperty.Register("MyInteger", typeof(Integer),typeof(MyControl), <MetaData>);
	
	    public int MyInteger
	    {
	    	get { return (bool)GetValue(MyIntegerProperty); }
	    	set { SetValue(MyIntegerProperty, value); }
	    }
    }
