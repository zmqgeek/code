            List<string> fruits = new List<string>();
            fruits.Add("Mango");
            fruits.Add("Banana");
            fruits.Add("Papaya");
            string commaSepFruits = string.Join(",", fruits.Select(f => "'" + f + "'"));
            Console.WriteLine(commaSepFruits);
            List<int> ids = new List<int>();
            ids.Add(1001);
            ids.Add(1002);
            ids.Add(1003);
            string commaSepIds = string.Join(",", ids);
            Console.WriteLine(commaSepIds);
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer { Id = 10001, Name = "John" });
            customers.Add(new Customer { Id = 10002, Name = "Robert" });
            customers.Add(new Customer { Id = 10002, Name = "Ryan" });
            string commaSepCustIds = string.Join(", ", customers.Select(cust => cust.Id));
            string commaSepCustNames = string.Join(", ", customers.Select(cust => "'" + cust.Name + "'"));
            Console.WriteLine(commaSepCustIds);
            Console.WriteLine(commaSepCustNames);
            Console.ReadLine();
