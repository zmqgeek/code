        static void Main(string[] args)
        {
            foreach (var trip in CreatePossibleTrips(trips))
            {
                // possible trip is actually calculated only at this point, because of yield
                if (IsTripGood(trip))
                {
                    // match good trip
                }
            }
        }
