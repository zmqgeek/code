    //Here we can return an IEnumerable of Election now, instead of using the Tuple class
    public static IEnumerable<Election> getElections()
    {
        using (var context = new ExampleDataContext())
        {
            return from election in context.Elections
                   select election;
        }
    }
    
    static void Main(string[] args)
    {
        //get the elections
        var elections = getElections();
    
        //lets go through the elections
        foreach (var election in elections)
        {
            //here we can access election status via the ElectionStatus property
            Console.WriteLine("Election status: {0}", election.ElectionStatus.StatusDescription);
        }
    }
