

using System.Text; 
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
   public class BukkenItiran 
    {
        public BukkenItiran()
        {

        }
        public void CSVFormExport(string SavePath, string IniPath, DataTable  dt)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
             File.WriteAllText(SavePath, sb.ToString());
        }
       
    }
}
