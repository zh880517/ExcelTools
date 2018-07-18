using ExcelDataReader;
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
                    if (val != null)
                    {
                        isNullRow = false;
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
    }
}
