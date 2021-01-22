    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Reflection;
    using System.IO;
    
    namespace SerializeStatic_NET
    {
        public class SerializeStatic
        {
            public static bool Save(Type static_class, string filename)
            {
                try
                {
                    FieldInfo[] fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);
                    object[,] a = new object[fields.Length,2];
                    int i = 0;
                    foreach (FieldInfo field in fields)
                    {
                        a[i, 0] = field.Name;
                        a[i, 1] = field.GetValue(null);
                        i++;
                    };
                    Stream f = File.Open(filename, FileMode.Create);
                    SoapFormatter formatter = new SoapFormatter();                
                    formatter.Serialize(f, a);
                    f.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
    
            public static bool Load(Type static_class, string filename)
            {
                try
                {
                    FieldInfo[] fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);                
                    object[,] a;
                    Stream f = File.Open(filename, FileMode.Open);
                    SoapFormatter formatter = new SoapFormatter();
                    a = formatter.Deserialize(f) as object[,];
                    f.Close();
                    if (a.GetLength(0) != fields.Length) return false;
                    int i = 0;
                    foreach (FieldInfo field in fields)
                    {
                        if (field.Name == (a[i, 0] as string))
                        {
                            field.SetValue(null, a[i,1]);
                        }
                        i++;
                    };                
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
