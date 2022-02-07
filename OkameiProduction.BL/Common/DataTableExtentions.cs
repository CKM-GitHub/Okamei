using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkameiProduction.BL
{
    public static class DataTableExtentions
    {
        public static T ToEntity<T>(this DataRow row) where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();
            var columns = row.Table.Columns;

            foreach (var property in properties)
            {
                if (!columns.Contains(property.Name)) continue;

                var value = row[property.Name];
                if (value == DBNull.Value)
                {
                    property.SetValue(obj, null);
                }
                else
                {
                    property.SetValue(obj, row[property.Name]);
                }
            }

            return obj;

        }

        public static IEnumerable<T> AsEnumerableEntity<T>(this DataTable dt) where T : new()
        {
            int i = 0;

            while (i < dt.Rows.Count)
            {
                var r = dt.Rows[i].ToEntity<T>();
                i++;
                yield return r;
            }
        }
    }
}
