    class Customer
    {
        public Customer(int id, string firstName, string LastName)
        {
            //assuming automatic properties
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
