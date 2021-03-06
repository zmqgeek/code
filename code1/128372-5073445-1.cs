    public static void Test()
    {
        var ccdc = new CounterCreationDataCollection();
        // Add the counter.
        const string counterName = "RateOfCountsPerSecond64Sample";
        var rateOfCounts64 = new CounterCreationData
        {
            CounterType = PerformanceCounterType.RateOfCountsPerSecond64,
            CounterName = counterName
        };
        ccdc.Add(rateOfCounts64);
        // Create the category.
        const string categoryName = "RateOfCountsPerSecond64SampleCategory";
        if (PerformanceCounterCategory.Exists(categoryName))
            PerformanceCounterCategory.Delete(categoryName);
        PerformanceCounterCategory.Create(categoryName, "",
            PerformanceCounterCategoryType.SingleInstance, ccdc);
        // create the counter
        var pc = new PerformanceCounter(categoryName, counterName, false);
        // send some sample data - ten counts per second
        while (true)
        {
            pc.IncrementBy(10);
            System.Threading.Thread.Sleep(1000);
        }
    }
