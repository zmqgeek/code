    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;
    public static class IEnumerableExt
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> things) where T : class
        {
            DataTable tbl = new System.Data.DataTable();
            bool buildColumns = false;
            foreach (var item in things)
            {
                Type t = item.GetType();
                var properties = t.GetProperties();
                if (!buildColumns)
                {
                    foreach (var prop in properties)
                    {
                        System.Data.DataColumn col = new DataColumn(prop.Name, prop.PropertyType);
                        tbl.Columns.Add(col);
                    }
                    buildColumns = true;
                }
                DataRow row = tbl.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item, null);
                }
                tbl.Rows.Add(row);
            }
            return tbl;
        }
    }
