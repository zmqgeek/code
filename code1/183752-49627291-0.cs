     public static T ConvertTo<T>(this object value)
               {
                   T returnValue;
        
                   if (value is T variable)
                       returnValue = variable;
                   else
                       try
                       {
                           //Handling Nullable types i.e, int?, double?, bool? .. etc
                           if (Nullable.GetUnderlyingType(typeof(T)) != null)
                           {
                               TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                               returnValue = (T) conv.ConvertFrom(value);
                           }
                           else
                           {
                               returnValue = (T) Convert.ChangeType(value, typeof(T));
                           }
                       }
                       catch (Exception)
                       {
                           returnValue = default(T);
                       }
        
                   return returnValue;
               }
