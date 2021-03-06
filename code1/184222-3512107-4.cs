    public static IQueryable<TEntity> WhereIn<TEntity, TValue>
     (
         this ObjectQuery<TEntity> query, 
         Expression<Func<TEntity, TValue>> selector, 
         IEnumerable<TValue> collection
     )
    {
        if (selector == null) throw new ArgumentNullException("selector");
        if (collection == null) throw new ArgumentNullException("collection");
        ParameterExpression p = selector.Parameters.Single();
        if (!collection.Any()) return query;
        IEnumerable<Expression> equals = collection.Select(value => 
          (Expression)Expression.Equal(selector.Body, 
           Expression.Constant(value, typeof(TValue))));
        Expression body = equals.Aggregate((accumulate, equal) => 
        Expression.Or(accumulate, equal));
        return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
    }
