        public static void Save(DataSet data, SqlConnection connection)
        {
            /// Dictionary for associating adapters to tables.
            Dictionary<DataTable, SqlDataAdapter> adapters = new Dictionary<DataTable, SqlDataAdapter>();
            foreach (DataTable table in data.Tables)
            {
                /// Find the table adapter using Reflection.
                Type adapterType = GetTableAdapterType(table);
                SqlDataAdapter adapter = SetupTableAdapter(adapterType, connection, validityEnd);
                adapters.Add(table, adapter);
            }
            /// Save the data.
            Save(data, adapters);
        }
        static Type GetTableAdapterType(DataTable table)
        {
            /// Find the adapter type for the table using the namespace conventions generated by dataset code generator.
            string nameSpace = table.GetType().Namespace;
            string adapterTypeName = nameSpace + "." + table.DataSet.DataSetName + "TableAdapters." + table.TableName + "TableAdapter";
            Type adapterType = Type.GetType(adapterTypeName);
            return adapterType;
        }
        static SqlDataAdapter SetupTableAdapter(Type adapterType, SqlConnection connection)
        {
            /// Set connection to TableAdapter and extract SqlDataAdapter (which is private anyway).
            object adapterObj = Activator.CreateInstance(adapterType);
            SqlDataAdapter sqlAdapter = (SqlDataAdapter)GetPropertyValue(adapterType, adapterObj, "Adapter");
            SetPropertyValue(adapterType, adapterObj, "Connection", connection);
            return sqlAdapter;
        }
        static object GetPropertyValue(Type type, object instance, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance).GetValue(instance, null);
        }
        static void SetPropertyValue(Type type, object instance, string propertyName, object propertyValue)
        {
            type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance).SetValue(instance, propertyValue, null);
        }
        static void Save(DataSet data, Dictionary<DataTable, SqlDataAdapter> adapters)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (adapters == null)
                throw new ArgumentNullException("adapters");
            Dictionary<DataTable, bool> procesedTables = new Dictionary<DataTable, bool>();
            List<DataTable> sortedTables = new List<DataTable>();
            while (true)
            {
                DataTable rootTable = GetRootTable(data, procesedTables);
                if (rootTable == null)
                    break;
                sortedTables.Add(rootTable);
            }
            /// Updating Deleted rows in Child -> Parent order.
            for (int i = sortedTables.Count - 1; i >= 0; i--)
            {
                Update(adapters, sortedTables[i], DataViewRowState.Deleted);
            }
            /// Updating Added / Modified rows in Parent -> Child order.
            for (int i = 0; i < sortedTables.Count; i++)
            {
                Update(adapters, sortedTables[i], DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
            }
        }
        static void Update(Dictionary<DataTable, SqlDataAdapter> adapters, DataTable table, DataViewRowState states)
        {
            SqlDataAdapter adapter = null;
            if (adapters.ContainsKey(table))
                adapter = adapters[table];
            if (adapter != null)
            {
                DataRow[] rowsToUpdate = table.Select("", "", states);
                if (rowsToUpdate.Length > 0)
                    adapter.Update(rowsToUpdate);
            }
        }
        static DataTable GetRootTable(DataSet data, Dictionary<DataTable, bool> procesedTables)
        {
            foreach (DataTable table in data.Tables)
            {
                if (!procesedTables.ContainsKey(table))
                {
                    if (IsRootTable(table, procesedTables))
                    {
                        procesedTables.Add(table, false);
                        return table;
                    }
                }
            }
            return null;
        }
        static bool IsRootTable(DataTable table, Dictionary<DataTable, bool> procesedTables)
        {
            foreach (DataRelation relation in table.ParentRelations)
            {
                DataTable parentTable = relation.ParentTable;
                if (parentTable != table && !procesedTables.ContainsKey(parentTable))
                    return false;
            }
            return true;
        }
