    var count = GetCriteria()
        .GetExecutableCriteria(session)
        .SetProjection(Projections.Count(Projections.Id()))
        .UniqueResult<long>();
    var result = GetCriteria()
        .GetExecutableCriteria(session)
        .SetFirstResult(0) 
        .SetMaxResults(10)
        .List<Entity>();
