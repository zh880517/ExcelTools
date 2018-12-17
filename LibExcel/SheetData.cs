using ExcelDataReader;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibExcel
{
    public class SheetData
    {
        private List<List<string>> data = new List<List<string>>();

        public string Name { get; private set; }
        public int ColumnCount { get; private set; }

        public int RowCount { get { return data.Count; } }

        internal SheetData(IExcelDataReader reader)
        {
            ColumnCount = reader.FieldCount;
            Name = reader.Name;
            while (reader.Read())
            {
                List<string> row = new List<string>(ColumnCount);
                bool isNullRow = true;
                for (int i = 0; i < ColumnCount; ++i)
                {
                    var val = reader.GetValue(i);
                    string strVal = "";
                    if (val is DateTime)
                    {
                        var fmt = reader.GetNumberFormatString(i);
                        strVal = ((DateTime)val).ToString(fmt.Replace('h', 'H'));
                    }
                    else
                    {
                        strVal = val.ToString();
                    }
                    row.Add(strVal);
                }
                if (!isNullRow)
                {
                    data.Add(row);
                }
            }
        }
        
        public string GetData(int row, int col)
        {
            if (row < data.Count && col < ColumnCount)
            {
                return data[row][col];
            }
            return null;
        }

        public string ToCSV(char sep = ',')
        {
            StringBuilder sb = new StringBuilder();
            foreach (var row in data)
            {
                sb.AppendJoin(sep, row).Append('\n');
            }
            return sb.ToString();
        }

        public JObject ToJson()
        {
            JObject json = new JObject();
            if (data.Count > 1)
            {
                var keyRow = data[0];
                JArray array = new JArray();
                for (int i=1; i<data.Count; ++i)
                {
                    var rowData = data[i];
                    if (rowData[0] != null && rowData[0].StartsWith("#"))
                        continue;
                    JObject rowJson = new JObject();
                    for (int j=0; j<keyRow.Count; ++j)
                    {
                        var key = keyRow[j];
                        if (!string.IsNullOrWhiteSpace(key))
                            continue;
                        rowJson[key] = rowData[j];
                    }
                    array.Add(rowJson);
                }
                json.Add("Values", array);
            }

            return json;
        }
    }
}
