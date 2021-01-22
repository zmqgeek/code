    public PropertyInfo GetPropertyInfo<TSource, TProperty>(
        TSource source,
        Expression<Func<TSource, TProperty>> propertyLambda)
    {
        Type type = typeof(TSource);
        if (!(propertyLambda.Body is MemberExpression member))
            throw new ArgumentException(string.Format(
                "Expression '{0}' refers to a method, not a property.",
                propertyLambda.ToString()));
        PropertyInfo propInfo = member.Member as PropertyInfo;
        if (propInfo == null)
            throw new ArgumentException(string.Format(
                "Expression '{0}' refers to a field, not a property.",
                propertyLambda.ToString()));
        if (type != propInfo.ReflectedType &&
            !type.IsSubclassOf(propInfo.ReflectedType))
            throw new ArgumentException(string.Format(
                "Expresion '{0}' refers to a property that is not from type {1}.",
                propertyLambda.ToString(),
                type));
        return propInfo;
    }
