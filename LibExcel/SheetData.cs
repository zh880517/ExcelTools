using ExcelDataReader;
using System.Collections.Generic;

namespace LibExcel
{
    public class SheetData
    {

        private string name;
        private int columnCount;
        private List<List<string>> data = new List<List<string>>();

        public string Name { get { return name; } }
        public int ColumnCount { get { return columnCount; } }

        public int RowCount { get { return data.Count; } }

        internal SheetData(IExcelDataReader reader)
        {
            columnCount = reader.FieldCount;
            name = reader.Name;
            while (reader.Read())
            {
                List<string> row = new List<string>(columnCount);
                bool isNullRow = true;
                for (int i = 0; i < columnCount; ++i)
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
            if (row < data.Count && col < columnCount)
            {
                return data[row][col];
            }
            return null;
        }
    }
}
