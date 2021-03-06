    private IQueryable<T> ReturnAGenericQuery<T>(IQueryable<T> source)
        where T : SomeBaseTypeForAllYourEntities
    {
        IQueryable<T> result =
            from refDataType in source
            where refDataType.Id > 0
            select refDataType;
        // Other stuff here
        return result;
    }
    public IList<Entity1> GetEntity1( ... )
    {
        return ReturnAGenericQuery(context.entity1).ToList();
    }
