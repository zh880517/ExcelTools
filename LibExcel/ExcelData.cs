using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

namespace LibExcel
{
    public class ExcelData
    {
        private string fullPath;
        private string name;
        private Dictionary<string, SheetData> data = new Dictionary<string, SheetData>();

        public string FullPath { get { return fullPath; } }
        public string Name { get { return name; } }

        internal ExcelData(string filePath)
        {
            fullPath = Path.GetFullPath(filePath);
            name = Path.GetFileNameWithoutExtension(fullPath);
        }

        public SheetData GetSheetData(string sheetName)
        {
            if (!data.ContainsKey(sheetName))
                ReadSheet(sheetName);
            data.TryGetValue(sheetName, out SheetData result);
            return result;
        }

        public void ReadSheet(HashSet<string> sheets)
        {
            using (var stream = File.Open(fullPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        if (sheets .Contains(reader.Name)&& !data.ContainsKey(reader.Name))
                        {
                            SheetData sheet = new SheetData(reader);
                            data.Add(sheet.Name, sheet);
                        }
                    } while (reader.NextResult());
                }
            }
        }

        public void ReadAll()
        {
            using (var stream = File.Open(fullPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        if (!data.ContainsKey(reader.Name))
                        {
                            SheetData sheet = new SheetData(reader);
                            data.Add(sheet.Name, sheet);
                        }
                    } while (reader.NextResult());
                }
            }
        }

        private void ReadSheet(string sheetName)
        {
            using (var stream = File.Open(fullPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do 
                    {
                        if (reader.Name == sheetName)
                        {
                            SheetData sheet = new SheetData(reader);
                            data.Add(sheet.Name, sheet);
                            break;
                        }
                    } while (reader.NextResult());
                }
            }
        }
    }
}
