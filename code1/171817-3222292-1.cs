        foreach (PropertyInfo prop in obj.GetType().GetProperties(
            BindingFlags.Instance | BindingFlags.Public))
        {
            if (prop.CanRead && prop.CanWrite && prop.PropertyType == typeof(string)
                && (prop.GetIndexParameters().Length == 0)) // watch for indexers!
            {
                var s = (string)prop.GetValue(obj, null);
                if (!string.IsNullOrEmpty(s)) s = s.Trim();
                prop.SetValue(obj, s, null);
            }
        }
