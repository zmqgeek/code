    public static void PrintLifeForms(IList<LifeForm> lifeForms)
    {
        foreach (var lifeForm in lifeForms)
        {
            Console.WriteLine(lifeForm.GetType().ToString());
        }
    }
