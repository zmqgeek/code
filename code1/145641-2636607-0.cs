    MethodInfo m = type.GetMethod(methodName);
            ParameterInfo[] pm = m.GetParameters();
            object ob;
            object[] y = new object[1];
            foreach (ParameterInfo paraminfo in pm)
            {
                ob = this.webServiceAssembly.CreateInstance(paraminfo.ParameterType.Name);
                foreach (PropertyInfo propera in ob.GetType().GetProperties())
                {
                    if (propera.Name == "AppGroupid")
                    {
                        propera.SetValue(ob, "SQL2005Tools", null);
                    }
                    if (propera.Name == "Appid")
                    {
                        propera.SetValue(ob, "%", null);
                    }
                }
                y[0] = ob;
            }
