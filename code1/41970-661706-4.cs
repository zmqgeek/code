    private delegate void SetPropertyThreadSafeDelegate<TResult>(Control @this, Expression<Func<TResult>> property, TResult value);
    public static void SetPropertyThreadSafe<TResult>(this Control @this, Expression<Func<TResult>> property, TResult value)
    {
      var propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;
      if (propertyInfo == null ||
          propertyInfo.ReflectedType != @this.GetType() ||
          @this.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
      {
        throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
      }
      if (@this.InvokeRequired)
      {
        @this.Invoke(new SetPropertyThreadSafeDelegate<TResult>(SetPropertyThreadSafe), new object[] { @this, propertyInfo.Name, value });
      }
      else
      {
        @this.GetType().InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, @this, new object[] { value });
      }
    }
