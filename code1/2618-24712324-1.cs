    public class ArgsSpecial :EventArgs
       {
            public ArgsSpecial (string val)
            {
                Operation=val;
            }
    
            public string Operation {get; set;}
       } 
  
     public class Animal
        {
           public event EventHandler<ArgsSpecial> Run = delegate{} //empty delegate. In this way you are sure that value is always != null because no one outside of the class can change it
            
           public void RaiseEvent()
           {  
              Run(this, new ArgsSpecial("Run faster"));
           }
        }
